using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Principal;

namespace ClientProxy
{
    public class NoProxyDuplexClientBase<TServiceInterface> : System.ServiceModel.DuplexClientBase<TServiceInterface>, ICustomClientBase<TServiceInterface> where TServiceInterface : class
    {
        public NoProxyDuplexClientBase(InstanceContext instanceContext)
            : base(instanceContext, typeof(TServiceInterface).Name)
        {

        }

        public NoProxyDuplexClientBase(InstanceContext instanceContext, string endpointConfigurationName) :
            base(instanceContext, endpointConfigurationName)
        {

        }

        public TServiceInterface GetServiceInstance()
        {
            return Channel;
        }

        public void SetAllowedImpersonationLevel(TokenImpersonationLevel? token)
        {
            ClientCredentials.Windows.AllowedImpersonationLevel = token ?? ClientCredentials.Windows.AllowedImpersonationLevel;
        }
    }
}
