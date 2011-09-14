using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientProxy
{
    public interface IProxyServices<TServiceInterface> : ILocateServices<TServiceInterface> where TServiceInterface : class
    {
        T Invoke<T>(Func<TServiceInterface, T> @delegate);
        void Invoke(Action<TServiceInterface> @delegate);
        void Invoke(Action<TServiceInterface> @delegate, System.Security.Principal.TokenImpersonationLevel? token);
        T Invoke<T>(Func<TServiceInterface, T> @delegate, System.Security.Principal.TokenImpersonationLevel? token);
        T InvokeAsUser<T>(Func<TServiceInterface, T> @delegate);
        void InvokeAsUser(Action<TServiceInterface> @delegate);
    }
}
