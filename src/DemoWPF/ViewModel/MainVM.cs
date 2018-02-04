using System.Collections.ObjectModel;
using System.ComponentModel;
using LightCommands.DemoWPF.Logic;

namespace LightCommands.DemoWPF.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MenuItemVM> Items { get; }

        public ObservableCollection<string> Output { get; }

        public MainVM()
        {
            Output = new ObservableCollection<string>();

            Items = new ObservableCollection<MenuItemVM>
                    {
                        new MenuItemVM(TestCmd.SayHello),
                        new MenuItemVM(TestCmd.SayRandom),
                        new MenuItemVM(TestCmd.Say, "Hi"),
                    };
        }
    }
}
