using System;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class GuidInstance
    {
        public const string ContextKey = "MiddlewareInjectedGuid";
        public Guid Value { get; } = Guid.NewGuid();

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
