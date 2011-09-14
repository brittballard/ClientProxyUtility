using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientProxy
{
    public interface ILocateServices<TServiceInterface> where TServiceInterface : class
    {
        Func<ICustomClientBase<TServiceInterface>> Locator { get; }
        Action<ICustomClientBase<TServiceInterface>> Destructor { get; }
    }
}
