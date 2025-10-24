using InMemory.Caching.Interfaces;
using InMemory.Caching.Models;
using InMemory.Caching.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(); // In-Memory Cache add

builder.Services.AddSingleton<IInMemoryCachingService, InMemoryCachingService>(); // CacheService Singleton injection

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


app.MapPost("/cache", (CacheModel cacheModel, IInMemoryCachingService cacheService) =>
{
    cacheService.Set(cacheModel.Key, cacheModel.Value);
    return Results.Ok();
});

app.MapGet("/cache/{key}", (string key, IInMemoryCachingService cacheService) =>
{
    var value = cacheService.Get(key);
    return value is not null ? Results.Ok(value) : Results.NotFound();
});

app.Run();