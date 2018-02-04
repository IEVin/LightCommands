using System.ComponentModel;

namespace LightCommands.DemoWPF.Logic
{
    public enum TestCmd
    {
        [Description("Coming Soon")]
        NotImplemented,

        [Description("Say hello!")]
        SayHello,

        [Description("Say random number")]
        SayRandom,

        [Description("Say ... ")]
        Say,

        [Description("Clear")]
        Clear,

        [Description("Exit")]
        Exit,
    }
}
