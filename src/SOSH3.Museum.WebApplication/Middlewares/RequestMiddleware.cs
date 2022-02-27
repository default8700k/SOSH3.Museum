using SOSH3.Museum.Database.Interfaces.Repositories;
using SOSH3.Museum.Database.Models;

namespace SOSH3.Museum.WebApplication.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate next;

        public RequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<RequestMiddleware> logger, IRequestRepository requestRepository)
        {
            var requestParams = new RequestParams
            {
                Ip = httpContext.Connection.RemoteIpAddress,
                Url = httpContext.Request.Path,
                Method = httpContext.Request.Method,
                UserAgent = httpContext.Request.Headers["User-Agent"],
                DateTime = DateTime.UtcNow
            };

            try
            {
                await requestRepository.AddRequestAsync(requestParams);
            }
            catch (Exception exception)
            {
                using var _loggerScope = logger.BeginScope(
                    new Dictionary<string, object?>
                    {
                        ["Ip"] = requestParams.Ip,
                        ["Url"] = requestParams.Url,
                        ["Method"] = requestParams.Method,
                        ["UserAgent"] = requestParams.UserAgent,
                        ["DateTime"] = requestParams.DateTime
                    }
                );

                logger.LogError(exception, "unhandled exception");
            }

            await next.Invoke(httpContext);
        }
    }
}
