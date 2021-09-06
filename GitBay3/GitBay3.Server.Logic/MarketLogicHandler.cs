using GitBay3.Common;
using GitBay3.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Logic
{
    public class MarketLogicHandler : IMarketLogicHandler
    {
        private IMarketPlace _marketPlace;

        public MarketLogicHandler() : this(MarketPlace.Instance)
        {

        }

        public MarketLogicHandler(IMarketPlace marketPlace)
        {
            _marketPlace = marketPlace;
        }

        public void SubscribeToMarketPlace(IMarketStateObserver marketStateObserver)
        {
            _marketPlace.Attach(marketStateObserver);
            _marketPlace.Run();
        }

        public void UnsubscribeToMarketPlace(IMarketStateObserver marketStateObserver)
        {
            _marketPlace.Detach(marketStateObserver);
            _marketPlace.End();
        }

        public float GetRate(Currencies currency)
        {
            return _marketPlace.GetRate(currency);
        }
    }
}
