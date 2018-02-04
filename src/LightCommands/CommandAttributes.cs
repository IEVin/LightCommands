using System;
using LightCommands.Implementation;

namespace LightCommands
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CmdExecuteAttribute : Attribute, ICommandAttribute
    {
        public Enum Command { get; }

        public CmdExecuteAttribute(object command)
        {
            Command = (Enum) command;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CmdCanExecuteAttribute : Attribute, ICommandAttribute
    {
        public Enum Command { get; }

        public CmdCanExecuteAttribute(object command)
        {
            Command = (Enum) command;
        }
    }
}