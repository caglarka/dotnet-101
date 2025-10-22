namespace MinimalApi.Filters;

public class LogFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Console.WriteLine($"Request: {context.HttpContext.Request.Path}");
        var result = await next(context);
        Console.WriteLine($"Response: {context.HttpContext.Response.StatusCode}");
        return result;
    }
}