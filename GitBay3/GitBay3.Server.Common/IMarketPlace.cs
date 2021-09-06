using GitBay3.Common;
using System.Collections.Generic;

namespace GitBay3.Server.Common
{
    public interface IMarketPlace
    {
        public void Run();
        public void End();
        public float GetRate(Currencies currency);
        public List<CurrencyEntity> GetAllRates();
    }
}
