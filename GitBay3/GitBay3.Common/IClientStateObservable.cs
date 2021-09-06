using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public interface IClientStateObservable
    {
        void Attach(IClientStateObserver clientStateObserver);
        void Detach(IClientStateObserver clientStateObserver);
        void Notify();
    }
}
