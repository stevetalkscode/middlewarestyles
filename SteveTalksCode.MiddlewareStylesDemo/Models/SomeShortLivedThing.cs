using System;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class SomeShortLivedThing : ISomeShortLivedThing
    {
        public SomeShortLivedThing(SomeLongLivedThing coreThing, string name)
        {
            CoreThing = coreThing;
            Name = name;
            UniqueIdentity = Guid.NewGuid();
        }
        public SomeLongLivedThing CoreThing { get; }
        public string Name { get; }
        public Guid UniqueIdentity { get; }
    }
}