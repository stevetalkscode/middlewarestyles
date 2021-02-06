using System;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public interface ISomeShortLivedThing
    {
        SomeLongLivedThing CoreThing { get; }
        string Name { get; }
        Guid UniqueIdentity { get; }
    }
}