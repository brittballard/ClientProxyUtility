using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientProxy
{
    public class ServiceProxy<TServiceInterface> : ServiceLocator<TServiceInterface>, IProxyServices<TServiceInterface> where TServiceInterface : class
    {
        private const string INVALID_INVOCATION_MESSAGE_FORMAT = "Invalid or failed invocation on type: {0}";
        private const string UNKNOWN_SERVICE_MESSAGE_FORMAT = "Could not locate service for type: {0}";

        public ServiceProxy()
            : base()
        {

        }

        public ServiceProxy(Func<TServiceInterface> @delegate)
            : base(@delegate)
        {

        }

        public ServiceProxy(Func<ICustomClientBase<TServiceInterface>> @delegate)
            : base(@delegate)
        {

        }

        //TODO (agts) : should prolly figure out how to cast the Action<T> as a Func<T, ?> to be more DRY
        public void Invoke(Action<TServiceInterface> @delegate)
        {
            Invoke(@delegate, null);
        }

        //TODO (agts) : should prolly figure out how to cast the Action<T> as a Func<T, ?> to be more DRY
        public void Invoke(Action<TServiceInterface> @delegate, System.Security.Principal.TokenImpersonationLevel? token)
        {
            var instance = Locator();
            instance.SetAllowedImpersonationLevel(token);

            try
            {
                @delegate(instance.GetServiceInstance());
            }
            finally
            {
                Destructor(instance);
            }
        }

        public T Invoke<T>(Func<TServiceInterface, T> @delegate)
        {
            return Invoke<T>(@delegate, null);
        }

        public T Invoke<T>(Func<TServiceInterface, T> @delegate, System.Security.Principal.TokenImpersonationLevel? token)
        {
            var instance = Locator();
            instance.SetAllowedImpersonationLevel(token);

            try
            {
                return @delegate(instance.GetServiceInstance());
            }
            finally
            {
                Destructor(instance);
            }
        }

        public void InvokeAsUser(Action<TServiceInterface> @delegate)
        {
            Invoke(@delegate, System.Security.Principal.TokenImpersonationLevel.Impersonation);
        }

        public T InvokeAsUser<T>(Func<TServiceInterface, T> @delegate)
        {
            return Invoke(@delegate, System.Security.Principal.TokenImpersonationLevel.Impersonation);
        }
    }
}
