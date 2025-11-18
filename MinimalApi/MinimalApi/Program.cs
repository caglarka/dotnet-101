using Microsoft.AspNetCore.Authorization;
using MinimalApi.Attributes;
using MinimalApi.Extensions;
using MinimalApi.Filters;
using MinimalApi.Models;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// # Note 8 [DI]
builder.Services.AddSingleton<UserService>();

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


// Note # 2 
// Route param
app.MapGet("/item/{id:int}", (int id) =>
    {
        if (id == 0)
            return Results.NotFound();

        return Results.Ok(new Item()
        {
            Id = id,
            Name = $"{id}"
        });
    })
    .AddEndpointFilter<LogFilter>() // # Note 5
    .WithName("Get Item") //Note # 4
    .WithSummary("Get a item by id")
    .WithSummary("Return the item object for given ID")
    .Produces<Item>()
    .Produces(StatusCodes.Status404NotFound);

// Query Param
app.MapGet("/search", (string? q) => $"Search -> {q}");

// Body Param
app.MapPost("/item", (Item item) => item);

// # Note 6
var userGroup = app.MapGroup("/user");

userGroup.MapGet(string.Empty, () => "All users");


// # Note 7
app.Use(async (_, next) =>
{
    Console.WriteLine("Before request");
    await next();
    Console.WriteLine("After request");
});

// # Note 8
userGroup.MapGet("/{id:int}", (int id, UserService userService) =>
{
    var user = userService.Get(id);

    return Results.Ok(user);
}).Produces<User>().Produces<User>(StatusCodes.Status404NotFound);

// # Note 9
app.MapGet("/ping", [Authorize, CustomMy]() => "pong")
    .RequireAuthorization(); // Alternative to [Authorize] attribute

// # Note 10
var v1 = app.MapGroup("/v1/users");
var v2 = app.MapGroup("/v2/users");

v1.MapGet("/", () => "v1 users");
v2.MapGet("/", () => "v2 users");

// # Note 11

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext _) => Results.Problem("An error occurred"));

app.MapGet("/secure", () => "Secret")  
    .RequireApiKey("123"); // # Note 12


app.Run();