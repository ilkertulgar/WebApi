namespace WebApi.Middleware;

public class FirstMiddleware
{
    private readonly RequestDelegate _next;
    public FirstMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Hello World");
        await _next.Invoke(context);
        Console.WriteLine("By World");
    }
    static public class HelloMiddlewareExtension
    {
        public static IApplicationBuilder UseHello(IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstMiddleware>();
        }
    }

}