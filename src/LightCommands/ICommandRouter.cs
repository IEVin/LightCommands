using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace LightCommands
{
    public interface ICommandRouter
    {
        IEnumerable<Enum> Commands { get; }

        bool RouteCommand(Enum command, [CanBeNull] object parameters = null);

        bool UpdateCommand(Enum command, [CanBeNull] object parameters = null);

        void SetErrorHandler(Action<object, Exception> handler);
    }
}