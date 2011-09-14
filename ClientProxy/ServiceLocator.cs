using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientProxy
{
    public class ServiceLocator<TServiceInterface> : ILocateServices<TServiceInterface> where TServiceInterface : class
    {
        private const string CANT_CLOSE_MESSAGE_FORMAT = "Could not Close() instance of type: {0}";
        private const string NONDESTRUCTIBLE_TYPE_MESSAGE_FORMAT = "Attempting to destruct non-destructible type: {0}";

        public ServiceLocator()
        {
            Locator = () => new NoProxyClientBase<TServiceInterface>();
        }

        public ServiceLocator(Func<TServiceInterface> @delegate)
        {
            Locator = () => new NoProxyClientBase<TServiceInterface>();
        }

        public ServiceLocator(Func<ICustomClientBase<TServiceInterface>> @delegate)
        {
            Locator = @delegate;
        }

        public Func<ICustomClientBase<TServiceInterface>> Locator { get; private set; }

        public Action<ICustomClientBase<TServiceInterface>> Destructor
        {
            get
            {
                return (iclient) =>
                {
                    dynamic client = iclient;
                    try
                    {
                        client.Close();
                    }
                    catch (Exception x)
                    {
                        try
                        {
                            client.Abort();
                        }
                        catch (Exception)
                        {
                            // TODO: (dbb) need to get some logging hooked up in here.
                        }
                    }
                };
            }
        }
    }
}
