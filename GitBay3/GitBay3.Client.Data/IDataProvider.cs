using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Data
{
    public interface IDataProvider
    {
        public float Buy(Currencies name, float amount);
        public float Sell(Currencies name, float amount);
        public Dictionary<Currencies, float> GetWallet();
        public bool Init(string uri);
        public void Dispose();
    }
}
