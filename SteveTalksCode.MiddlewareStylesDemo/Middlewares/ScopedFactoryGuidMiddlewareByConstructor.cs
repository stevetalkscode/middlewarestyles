using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    [UsedImplicitly]
    public class ScopedFactoryGuidMiddlewareByConstructor : IMiddleware
    {
        private readonly GuidInstance _guidInstance;
        public ScopedFactoryGuidMiddlewareByConstructor(GuidInstance guidInstance)
        {
            _guidInstance = guidInstance;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(ScopedFactoryGuidMiddlewareByConstructor)}", $"By Factory Constructor : {_guidInstance} - Scoped as registered as scoped and guid injected from scope");
            await next(context);
        }
    }
}