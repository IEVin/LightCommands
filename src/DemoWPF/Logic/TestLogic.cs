using System;
using System.Linq;
using LightCommands.DemoWPF.ViewModel;

namespace LightCommands.DemoWPF.Logic
{
    public class TestLogic : CommandRouter
    {
        private readonly MainVM model;

        public TestLogic(MainVM model)
        {
            this.model = model;
        }

        [CmdCanExecute(TestCmd.SayHello)]
        [CmdCanExecute(TestCmd.SayRandom)]
        [CmdCanExecute(TestCmd.Say)]
        public bool CanSay(object parameters)
        {
            return model != null;
        }

        [CmdExecute(TestCmd.SayHello)]
        public void SayHelloCmd(object parameters)
        {
            SayCmd("Hello!");
        }

        [CmdExecute(TestCmd.SayRandom)]
        public void SayRandomCmd(object parameters)
        {
            var randomNumber = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            SayCmd($"Number {randomNumber}!");
        }

        [CmdExecute(TestCmd.Say)]
        public void SayCmd(object parameters)
        {
            model.Output.Add(Convert.ToString(parameters));
        }


        [CmdCanExecute(TestCmd.Clear)]
        public bool CanClear(object parameters)
        {
            return model != null && model.Output.Any();
        }

        [CmdExecute(TestCmd.Clear)]
        public void ClearCmd(object parameters)
        {
            model.Output.Clear();
        }

        [CmdExecute(TestCmd.Exit)]
        public void ExitCmd(object parameters)
        {
            Environment.Exit(0);
        }
    }
}
