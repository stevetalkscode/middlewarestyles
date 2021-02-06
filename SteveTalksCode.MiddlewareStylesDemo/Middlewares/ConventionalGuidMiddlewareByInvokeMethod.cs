using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class ConventionalGuidMiddlewareByInvokeMethod
    {
        private readonly RequestDelegate _next;

        public ConventionalGuidMiddlewareByInvokeMethod(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext context, GuidInstance guidInstance)
        {
            var localGuidInstance = guidInstance ?? throw new ArgumentNullException(nameof(guidInstance));
            context.Items.Add($"{GuidInstance.ContextKey}_{nameof(ConventionalGuidMiddlewareByInvokeMethod)}", $"By Convention Invoke : {localGuidInstance} - SCOPED - changes on each request");
            await _next(context);
        }
    }
}