namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class ShortLivedThingFactory
    {
        private readonly SomeLongLivedThing _coreThing;
        public ShortLivedThingFactory(SomeLongLivedThing coreThing) => _coreThing = coreThing;
        public SomeShortLivedThing MakeShortLivedThing(string name)
            => new (_coreThing, name);
    }
}