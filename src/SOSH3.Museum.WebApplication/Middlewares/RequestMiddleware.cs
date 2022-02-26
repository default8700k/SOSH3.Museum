namespace SOSH3.Museum.WebApplication.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate next;

        public RequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await next.Invoke(httpContext);
        }
    }
}
