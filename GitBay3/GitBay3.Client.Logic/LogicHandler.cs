using GitBay3.Client.Data;
using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Logic
{
    public class LogicHandler : ILogicHandler
    {
        private IMarketProvider _marketProvider;
        private IDataProvider _dataProvider;

        public LogicHandler() : this(new DataProvider(), new MarketProvider())
        {

        }

        public LogicHandler(IDataProvider dataProvider, IMarketProvider marketProvider)
        {
            _marketProvider = marketProvider;
            _dataProvider = dataProvider;
        }

        public float Buy(Currencies name, float amount)
        {
            return _dataProvider.Buy(name, amount);
        }

        public float Sell(Currencies name, float amount)
        {
            return _dataProvider.Sell(name, amount);
        }

        public Dictionary<Currencies, float> GetWallet()
        {
            return _dataProvider.GetWallet();
        }

        //public Dictionary<Currencies, float> GetMarket()
        //{
        //    var result = new Dictionary<Currencies, float>();
        //    foreach (Currencies c in Enum.GetValues(typeof(Currencies)))
        //    {
        //        result.Add(c, 0);
        //    }
        //    return result;
        //}

        public void SubscribeToStateChanges(IClientStateObserver clientStateObserver)
        {
            State.Instance.Attach(clientStateObserver);
            if (_marketProvider.Init())
            {
                _dataProvider.Init(_marketProvider.GetUri());
            }
        }

        public void UnsubscribeFromStateChanges(IClientStateObserver clientStateObserver)
        {
            State.Instance.Detach(clientStateObserver);
            _marketProvider.Dispose();
            _dataProvider.Dispose();
        }
    }
}
