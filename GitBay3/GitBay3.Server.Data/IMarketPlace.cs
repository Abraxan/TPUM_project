using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Data
{
    public interface IMarketPlace
    {
        public void Run();
        public void End();
        public float GetRate(Currencies currency);
        public List<CurrencyEntity> GetAllRates();
        public void Attach(IMarketStateObserver marketStateObserver);
        public void Detach(IMarketStateObserver marketStateObserver);
        public void Notify();
    }
}
