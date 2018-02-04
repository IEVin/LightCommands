using System;

namespace LightCommands.Implementation
{
    public struct RouteHandler
    {
        public Action<object> Executor { get; }
        public Predicate<object> Updater { get; }

        public RouteHandler(Action<object> executor, Predicate<object> updater)
        {
            Executor = executor;
            Updater = updater;
        }
    }
}
