using GitBay3.Client.Logic;
using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client
{
    public class Model : IClientStateObserver
    {
        private ILogicHandler _handler;
        public ObservableCollection<Entry> Wallet { get; set; }
        public ObservableCollection<Entry> Market { get; set; }

        public Model() : this(new LogicHandler())
        {

        }

        public Model(ILogicHandler handler)
        {
            Wallet = new ObservableCollection<Entry>();
            Market = new ObservableCollection<Entry>();
            _handler = handler;
        }

        public void Buy(Currencies name, float amount)
        {
            float result = _handler.Buy(name, amount);
            UpdateWalletEntry(name, amount);
            UpdateWalletEntry(Currencies.PLN, -result);
        }

        public void Sell(Currencies name, float amount)
        {
            float result = _handler.Sell(name, amount);
            UpdateWalletEntry(name, -amount);
            UpdateWalletEntry(Currencies.PLN, result);
        }

        public void LoadAccount()
        {
            _handler.SubscribeToStateChanges(this);
            var resultW = _handler.GetWallet();
            foreach (var k in resultW.Keys)
            {
                Wallet.Add(new Entry() { Currency = k, Value = resultW[k] });
            }
        }

        private void UpdateWalletEntry(Currencies currency, float amount)
        {
            var entry = Wallet.Single(e => e.Currency == currency);
            var indexOf = Wallet.IndexOf(entry);
            var newEntry = new Entry() { Currency = entry.Currency, Value = entry.Value + amount };
            Wallet[indexOf] = newEntry;
        }

        private void UpdateMarketEntry(Currencies currency, float value)
        {
            var newEntry = new Entry() { Currency = currency, Value = value };
            if (Market.Any(e => e.Currency == currency))
            {
                var entry = Market.Single(e => e.Currency == currency);
                var indexOf = Market.IndexOf(entry);
                Market[indexOf] = newEntry;
            }
            else
            {
                Market.Add(newEntry);
            }
        }

        public void StateUpdate(Dictionary<Currencies, float> updatedRates)
        {
            foreach (var r in updatedRates)
            {
                UpdateMarketEntry(r.Key, r.Value);
            }
        }

        public void Close()
        {
            _handler.UnsubscribeFromStateChanges(this);
        }
    }
}
