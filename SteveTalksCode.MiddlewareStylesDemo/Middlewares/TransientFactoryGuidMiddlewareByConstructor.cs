using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    [UsedImplicitly]
    public class TransientFactoryGuidMiddlewareByConstructor : IMiddleware
    {
        private readonly GuidInstance _guidInstance;
        public TransientFactoryGuidMiddlewareByConstructor(GuidInstance guidInstance)
        {
            _guidInstance = guidInstance;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(TransientFactoryGuidMiddlewareByConstructor)}", $"By Factory Constructor : {_guidInstance} - Still scoped as registered as transient, but guid injected from scope");
            await next(context);
        }
    }
}