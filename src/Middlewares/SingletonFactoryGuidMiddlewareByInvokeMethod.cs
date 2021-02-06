using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class SingletonFactoryGuidMiddlewareByInvokeMethod : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            GuidInstance guidInstance = context.RequestServices.GetRequiredService<GuidInstance>();
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(SingletonFactoryGuidMiddlewareByInvokeMethod)}", $"By Factory Invoke Method : {guidInstance}");
            await next(context);
        }
    }
}