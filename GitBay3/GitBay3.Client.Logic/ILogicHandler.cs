using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Logic
{
    public interface ILogicHandler
    {
        public float Buy(Currencies name, float amount);
        public float Sell(Currencies name, float amount);
        public Dictionary<Currencies, float> GetWallet();
        //public Dictionary<Currencies, float> GetMarket();
        public void SubscribeToStateChanges(IClientStateObserver clientStateObserver);
        public void UnsubscribeFromStateChanges(IClientStateObserver clientStateObserver);
    }
}
