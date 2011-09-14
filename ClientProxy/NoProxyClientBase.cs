using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace ClientProxy
{
    internal class NoProxyClientBase<TServiceInterface> : System.ServiceModel.ClientBase<TServiceInterface>,
                                                       ICustomClientBase<TServiceInterface>
        where TServiceInterface : class
    {
        public NoProxyClientBase()
            : base(typeof(TServiceInterface).Name)
        {
        }

        public NoProxyClientBase(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public TServiceInterface GetServiceInstance()
        {
            return Channel;
        }

        public void SetAllowedImpersonationLevel(TokenImpersonationLevel? token)
        {
            ClientCredentials.Windows.AllowedImpersonationLevel = token ?? ClientCredentials.Windows.AllowedImpersonationLevel; ;
        }
    }
}
