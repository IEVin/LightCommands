using System.Windows.Input;

namespace LightCommands.WPF.Hotkeys
{
    public class HotKeyParams
    {
        public Key Key { get; }
        public ModifierKeys Modifiers { get; }
        public object Parameter { get; }

        public HotKeyParams( Key key = Key.None, ModifierKeys modifiers = ModifierKeys.None, object parameter = null )
        {
            Key = key;
            Modifiers = modifiers;
            Parameter = parameter;
        }
    }
}