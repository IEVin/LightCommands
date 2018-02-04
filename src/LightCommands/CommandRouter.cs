using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LightCommands.Implementation;

namespace LightCommands
{
    public class CommandRouter : ICommandRouter
    {
        private readonly IDictionary<Enum, RouteHandler> handlers;
        private Action<object, Exception> errorHandler;

        public virtual IEnumerable<Enum> Commands => handlers.Keys;

        protected CommandRouter()
        {
            handlers = CommandHelper.CreateHandlers(this);
            SetErrorHandler(null);
        }

        public CommandRouter([NotNull] object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            handlers = CommandHelper.CreateHandlers(obj);
            SetErrorHandler(null);
        }

        public bool RouteCommand(Enum command, object parameters = null)
        {
            RouteHandler handler;
            if (!handlers.TryGetValue(command, out handler))
                return false;

            try
            {
                handler.Executor(parameters);
                return true;
            }
            catch (Exception ex)
            {
                errorHandler?.Invoke(this, ex);
                return false;
            }
        }

        public bool UpdateCommand(Enum command, object parameters = null)
        {
            RouteHandler handler;
            if (!handlers.TryGetValue(command, out handler))
                return false;

            try
            {
                return handler.Updater.Invoke(parameters);
            }
            catch (Exception ex)
            {
                errorHandler?.Invoke(this, ex);
                return false;
            }
        }

        public void SetErrorHandler(Action<object, Exception> handler)
        {
            errorHandler = handler ?? ((o, ex) => { throw ex; });
        }
    }
}
