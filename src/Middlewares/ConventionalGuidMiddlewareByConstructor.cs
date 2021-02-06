using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace SteveTalksCode.MiddlewareStylesDemo
{
public class ConventionalGuidMiddlewareByConstructor
{
    private readonly GuidInstance _guidInstance;
    private readonly RequestDelegate _next;

    public ConventionalGuidMiddlewareByConstructor(RequestDelegate next, GuidInstance guidInstance)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _guidInstance = guidInstance ?? throw new ArgumentNullException(nameof(guidInstance));

    }

    [UsedImplicitly]
    public async Task InvokeAsync(HttpContext context)
    {
        context.Items.Add($"{GuidInstance.ContextKey}_{nameof(ConventionalGuidMiddlewareByConstructor)}", $"By Convention Constructor : {_guidInstance}" );
        await _next(context);
    }
}
}