using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public interface IMarketStateObserver
    {
        void StateUpdate(Dictionary<Currencies, float> updatedRates);
    }
}
