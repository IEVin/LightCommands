using System.Windows;
using LightCommands.DemoWPF.Logic;
using LightCommands.DemoWPF.ViewModel;
using LightCommands.WPF;

namespace LightCommands.DemoWPF
{
    public partial class MainWindow : Window
    {
        private readonly MainVM viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainVM();
            DataContext = viewModel;

            // can be injected in constructor or be a part of view model
            var logic = new TestLogic(viewModel);
            logic.FillCommands(CommandBindings);
        }
    }
}
