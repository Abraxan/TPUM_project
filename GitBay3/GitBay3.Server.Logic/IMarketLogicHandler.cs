using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Logic
{
    public interface IMarketLogicHandler
    {
        public void SubscribeToMarketPlace(IMarketStateObserver marketStateObserver);
        public void UnsubscribeToMarketPlace(IMarketStateObserver marketStateObserver);

    }
}
