using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Data
{
    public class State : IClientStateObservable //singleton
    {
        private static State _instance = null;
        private static readonly object _lock = new object();
        private static readonly object _dataLock = new object();
        private static readonly object _oLock = new object();
        private Dictionary<Currencies, float> _marketState;
        private List<IClientStateObserver> _observers;

        public static State Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new State();
                        }
                    }
                }
                return _instance;
            }
        }

        private State()
        {
            _marketState = new Dictionary<Currencies, float>();
            _observers = new List<IClientStateObserver>();
        }

        public void SetMarket(List<Entry> values)
        {
            foreach(var v in values)
            {
                Update(v);
            }
        }

        private void Update(Entry entry)
        {
            lock (_dataLock)
            {
                if (_marketState.ContainsKey(entry.Currency))
                {
                    _marketState[entry.Currency] = entry.Value;
                }
                else
                {
                    _marketState.Add(entry.Currency, entry.Value);
                }
            }
            Notify();
        }

        public void Attach(IClientStateObserver clientStateObserver)
        {
            lock (_oLock)
            {
                if (!_observers.Contains(clientStateObserver))
                {
                    _observers.Add(clientStateObserver);
                }
            }
        }

        public void Detach(IClientStateObserver clientStateObserver)
        {
            lock (_oLock)
            {
                if (_observers.Contains(clientStateObserver))
                {
                    _observers.Remove(clientStateObserver);
                }
            }
        }

        public void Notify()
        {
            lock (_oLock)
            {
                foreach (var o in _observers)
                {
                    o.StateUpdate(_marketState);
                }
            }
        }
    }
}
