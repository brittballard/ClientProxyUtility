using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientProxy
{
    public interface ICustomClientBase<TServiceInterface> where TServiceInterface : class
    {
        TServiceInterface GetServiceInstance();
        void SetAllowedImpersonationLevel(System.Security.Principal.TokenImpersonationLevel? token);
        void Close();
        void Abort();
    }
}
