using GitBay3.Common;
using System.Collections.Generic;
using System.Linq;

namespace GitBay3.Server.Data
{
    public class Account : AccountEntity
    {
        public Account(string name)
        {
            Name = name;
            Wallet = new Dictionary<Currencies, float>();
            Wallet.Add(0, 100000);
            Wallet.Add(Currencies.BTC, 0);
            Wallet.Add(Currencies.ETH, 0);
            Wallet.Add(Currencies.LTC, 0);
        }

        public override void ChangeBalance(Currencies currency, float value)
        {
            if (Wallet.Keys.Any(k => k == currency))
            {
                Wallet[currency] += value;
            }
            else
            {
                Wallet.Add(currency, value);
            }
        }
    }
}
