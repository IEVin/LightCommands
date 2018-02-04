using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace LightCommands.WPF.Hotkeys
{
    public interface IHotKeyResolver
    {
        IEnumerable<HotKeyParams> ResolveKeys([NotNull] Enum enumCommand);
    }
}
