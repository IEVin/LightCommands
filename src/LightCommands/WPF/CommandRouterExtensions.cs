using System;
using System.Collections.Generic;
using System.Windows.Input;
using JetBrains.Annotations;
using LightCommands.Implementation;
using LightCommands.WPF.Hotkeys;

namespace LightCommands.WPF
{
    public static class CommandRouterExtensions
    {
        private static readonly Dictionary<Enum, RoutedUICommand> commands = new Dictionary<Enum, RoutedUICommand>();

        [NotNull]
        public static RoutedUICommand GetRoutedUICommand(this Enum command)
        {
            return commands.GetValueOrCreate(command, x => new RoutedUICommand(x.GetCommandName(), x.ToString(), typeof(CommandHelper)));
        }

        [NotNull]
        public static string GetCommandName(this Enum command)
        {
            return command.GetDescription();
        }

        public static void FillCommands([NotNull] this ICommandRouter router, [NotNull] CommandBindingCollection bindings)
        {
            if (router == null)
                throw new ArgumentNullException(nameof(router));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));

            foreach (var q in router.Commands)
            {
                var closure = q;
                var binding = new CommandBinding(GetRoutedUICommand(q),
                                                 (o, e) => router.RouteCommand(closure, e.Parameter),
                                                 (o, e) => e.CanExecute = router.UpdateCommand(closure, e.Parameter));
                bindings.Add(binding);
            }
        }

        public static void FillHotkeys([NotNull] this ICommandRouter router, [NotNull] IHotKeyResolver keyResolver, [NotNull] InputBindingCollection bindings)
        {
            if (router == null)
                throw new ArgumentNullException(nameof(router));
            if (keyResolver == null)
                throw new ArgumentNullException(nameof(keyResolver));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));

            foreach (var q in router.Commands)
            {
                var parameters = keyResolver.ResolveKeys(q);
                foreach (var prm in parameters)
                {
                    bindings.Add(new SimpleKeyBinding
                                 {
                                     Command = GetRoutedUICommand(q),
                                     Key = prm.Key,
                                     Modifiers = prm.Modifiers,
                                     CommandParameter = prm.Parameter
                                 });
                }
            }
        }
    }
}