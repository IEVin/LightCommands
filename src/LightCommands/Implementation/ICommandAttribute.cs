using System;

namespace LightCommands.Implementation
{
    internal interface ICommandAttribute
    {
        Enum Command { get; }
    }
}
