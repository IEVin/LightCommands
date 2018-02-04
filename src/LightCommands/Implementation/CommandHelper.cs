using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LightCommands.Implementation
{
    public static class CommandHelper
    {
        public static IDictionary<Enum, RouteHandler> CreateHandlers(object obj)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            var methods = GetMethodsRecursive(obj.GetType(), flags).ToList();

            // create handlers and updaters list
            var updaters = GetHandlers<Predicate<object>, Predicate<object>, CmdCanExecuteAttribute>(obj, methods, (cmd, act) => (Predicate<object>) act);
            return GetHandlers<RouteHandler, Action<object>, CmdExecuteAttribute>(obj, methods, (cmd, act) =>
                                                                                                {
                                                                                                    var updater = updaters.GetValueOrDefault(cmd, DefaultUpdater);
                                                                                                    return new RouteHandler((Action<object>) act, updater);
                                                                                                });
        }

        private static IEnumerable<MethodInfo> GetMethodsRecursive(Type type, BindingFlags flags)
        {
            if (type == null)
                return Enumerable.Empty<MethodInfo>();

            var baseMethods = GetMethodsRecursive(type.BaseType, flags);
            return type.GetMethods(flags).Concat(baseMethods);
        }

        private static IDictionary<Enum, TResult> GetHandlers<TResult, TDelegate, TAttribute>(object obj, IEnumerable<MethodInfo> methods, Func<Enum, Delegate, TResult> func)
            where TAttribute : Attribute, ICommandAttribute
        {
            var dict = new Dictionary<Enum, TResult>();
            foreach (var mi in methods)
            {
                var names = Attribute.GetCustomAttributes(mi, typeof(TAttribute), true)
                                     .OfType<ICommandAttribute>()
                                     .Select(x => x.Command);

                foreach (var q in names)
                {
                    var @this = mi.IsStatic ? null : obj;
                    var cmd = Delegate.CreateDelegate(typeof(TDelegate), @this, mi, false);
                    if (cmd == null)
                        continue;

                    dict[q] = func(q, cmd);
                }
            }

            return dict;
        }

        [DebuggerHidden]
        private static bool DefaultUpdater(object parameters)
        {
            return true;
        }
    }
}
