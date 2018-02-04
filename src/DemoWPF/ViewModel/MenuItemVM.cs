using System;
using System.ComponentModel;
using LightCommands.WPF;

namespace LightCommands.DemoWPF.ViewModel
{
    public class MenuItemVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Enum Command { get; }
        public object Parameter { get; }

        public string Name => Command.GetCommandName();

        public MenuItemVM(Enum command, object parameter = null)
        {
            Command = command;
            Parameter = parameter;
        }
    }
}
