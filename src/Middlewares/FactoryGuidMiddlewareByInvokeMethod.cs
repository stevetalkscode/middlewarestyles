using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class FactoryGuidMiddlewareByInvokeMethod : IMiddleware
    {
        private readonly GuidInstance _guidInstance;

        public FactoryGuidMiddlewareByInvokeMethod(GuidInstance guidInstance)
        {
            _guidInstance = guidInstance ?? throw new ArgumentNullException(nameof(guidInstance));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(FactoryGuidMiddlewareByInvokeMethod)}",
                $"By Factory Constructor : {_guidInstance}");
            await next(context);
        }
    }

    public class LogUserAgentMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public LogUserAgentMiddleware(ILogger<LogUserAgentMiddleware> logger)
        {
            // The null check here is belt and braces in case there is a
            // custom middleware factory that uses GetService instead of
            // GetRequiredService. If using the default, this check is not
            // required.
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userAgent = context.Request.Headers.GetCommaSeparatedValues("User-Agent").LastOrDefault();
            if (userAgent is not null) _logger?.LogTrace($"User Agent : {userAgent}");
            await next(context);
        }
    }
}