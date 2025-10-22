namespace MinimalApi.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder RequireApiKey(this RouteHandlerBuilder builder, string key)
    {
        return builder.AddEndpointFilter(async (ctx, next) =>
        {
            var headers = ctx.HttpContext.Request.Headers;
            if (!headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != key)
            {
                return Results.Unauthorized();
            }

            return await next(ctx);
        });
    }
}