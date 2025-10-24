using Distributed.Caching.Interfaces;
using Distributed.Caching.Models;
using Distributed.Caching.Models.Options;
using Distributed.Caching.Services;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<RedisOptions>()
    .Bind(builder.Configuration.GetSection("RedisOptions"))
    .Validate(options => options.Hosts.Count > 0, "At least one Redis host is required.")
    .ValidateOnStart();

builder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
{
    var redisOptions = serviceProvider.GetRequiredService<IOptions<RedisOptions>>().Value;

    var options = new ConfigurationOptions()
    {
        Password = redisOptions.Password,
        Ssl = redisOptions.Ssl,
        DefaultDatabase = redisOptions.Database,
        ConnectTimeout = redisOptions.ConnectTimeout,
        ConnectRetry = redisOptions.ConnectRetry,
        AbortOnConnectFail = false
    };

    foreach (var host in redisOptions.Hosts)
    {
        options.EndPoints.Add(host.Hostname, host.Port);
    }

    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddScoped<IDistributedCachingService, DistributedCachingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

#region Endpoints

app.MapPost("/cache",
    async (CacheModel<string> cacheModel,
        IDistributedCachingService distributedCachingService,
        IOptions<RedisOptions> redisOptions) =>
    {
        var ttlOffset = new TimeSpan(0, 0, redisOptions.Value.TtlMinutes, 0);
        await distributedCachingService.SetValueAsync(cacheModel.Key, cacheModel.Value, ttlOffset);
    });

app.MapGet("/cache/{key}", async (string key, IDistributedCachingService distributedCachingService) =>
{
    var value = await distributedCachingService.GetValueAsync(key);

    if (value is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(value);
});

#endregion

app.Run();