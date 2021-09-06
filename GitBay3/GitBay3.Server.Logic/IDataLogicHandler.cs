using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Logic
{
    public interface IDataLogicHandler
    {
        float ProcessPurchaseRequest(Currencies name, float value);
        float ProcessSaleRequest(Currencies name, float value);
        public Dictionary<Currencies, float> GetWallet();
        public void Init();
    }
}
