using System;
using Microsoft.Extensions.DependencyInjection;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class ShortLivedThingFactory2
    {
        private readonly IServiceProvider _sp;
        public ShortLivedThingFactory2(IServiceProvider sp) => _sp = sp;

        public ISomeShortLivedThing MakeShortLivedThing(string name)
            => ActivatorUtilities.CreateInstance<ISomeShortLivedThing>(_sp, name);
    }
}