using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public interface IServerStateObservable
    {
        void Attach(IMarketStateObserver serverStateObserver);
        void Detach(IMarketStateObserver serverStateObserver);
        void Notify();
    }
}
