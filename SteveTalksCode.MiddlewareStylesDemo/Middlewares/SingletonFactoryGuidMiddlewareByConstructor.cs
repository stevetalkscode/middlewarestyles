using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    [UsedImplicitly]
    public class SingletonFactoryGuidMiddlewareByConstructor : IMiddleware
    {
        private readonly GuidInstance _guidInstance;
        public SingletonFactoryGuidMiddlewareByConstructor(GuidInstance guidInstance)
        {
            _guidInstance = guidInstance;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(SingletonFactoryGuidMiddlewareByConstructor)}", $"By Factory Constructor : {_guidInstance} - CAPTURED as registered as singleton and guid injected");
            await next(context);
        }
    }
}