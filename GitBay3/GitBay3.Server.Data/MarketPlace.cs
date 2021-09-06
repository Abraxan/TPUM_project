using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GitBay3.Server.Data
{
    public class MarketPlace : IMarketPlace
    {
        protected static object _ratesLock = new object();
        private Dictionary<Currencies, float> _rates;
        private Thread _simulation;
        private bool _simulationSwitch = false;
        private List<IMarketStateObserver> _observers;

        private static MarketPlace _instance = null;
        private static readonly object _lock = new object();
        private bool _hasStarted = false;

        public static MarketPlace Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MarketPlace();
                        }
                    }
                }
                return _instance;
            }
        }

        private MarketPlace() 
        {
            _rates = GenerateRates();
            _simulation = new Thread(new ThreadStart(Simulate));
            _observers = new List<IMarketStateObserver>();
        }

        private static Dictionary<Currencies, float> GenerateRates()
        {
            var rates = new Dictionary<Currencies, float>();
            var valueGenerator = new Random((int)DateTime.Now.Ticks);
            lock (_ratesLock)
            {
                foreach (Currencies c in Enum.GetValues(typeof(Currencies)))
                {
                    if (c != 0)
                    {
                        var value = (((float)valueGenerator.NextDouble() * 10) - 5) * 0.01f;
                        rates.Add(c, value);
                    }
                }
            }
            return rates;
        }

        public void Run()
        {
            if (!_hasStarted)
            {
                _simulationSwitch = true;
                _simulation.Start();
                _hasStarted = true;
            }
        }

        public void End()
        {
            if (_hasStarted)
            {
                _simulation.Interrupt(); // wymuszenie wyjątku
                _simulationSwitch = false;
                _simulation.Join(); // oczekiwanie na zakończenie symulacji 
            }
        }

        public float GetRate(Currencies currency)
        {
            return _rates[currency];
        }

        public List<CurrencyEntity> GetAllRates()
        {
            var list = new List<CurrencyEntity>();
            foreach (var k in _rates.Keys)
            {
                list.Add(new Currency(k, _rates[k]));
            }
            return list;
        }

        private void Simulate()
        {
            while (_simulationSwitch)
            {
                var isRaisingGenerator = new Random((int)DateTime.Now.Ticks);
                var valueChangeGenerator = new Random((int)DateTime.Now.Ticks + 666);
                lock (_ratesLock)
                {
                    foreach (var k in _rates.Keys)
                    {
                        bool isRaising = isRaisingGenerator.Next(100) <= 25;
                        float valueChange = (((float)valueChangeGenerator.NextDouble() * 10) - 5) * 0.01f;
                        if (isRaising)
                        {
                            _rates[k] += valueChange;
                        }
                        else
                        {
                            var temp = _rates[k] - valueChange;
                            if (temp > 0)
                            {
                                _rates[k] = temp;
                            }
                            else
                            {
                                _rates[k] += (temp / 2);
                            }
                        }
                    }
                    Notify();
                }
                try
                {
                    Thread.Sleep(1500);
                }
                catch (ThreadInterruptedException)
                {
                    //wątek nie może usnąć zakłócony przez główny wątek
                }

            }
        }

        public void Attach(IMarketStateObserver marketStateObserver)
        {
            if (!_observers.Contains(marketStateObserver))
            {
                _observers.Add(marketStateObserver);
            }
        }

        public void Detach(IMarketStateObserver marketStateObserver)
        {
            if (_observers.Contains(marketStateObserver))
            {
                _observers.Remove(marketStateObserver);
            }
        }

        public void Notify()
        {
            foreach(var o in _observers)
            {
                o.StateUpdate(_rates);
            }
        }
    }
}
