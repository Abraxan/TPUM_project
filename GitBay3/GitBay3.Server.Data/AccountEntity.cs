using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Data
{
    public abstract class AccountEntity
    {
        private string _name;
        private Dictionary<Currencies, float> _wallet;

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public Dictionary<Currencies, float> Wallet
        {
            get { return _wallet; }
            protected set { _wallet = value; }
        }

        public abstract void ChangeBalance(Currencies currency, float value);

    }
}
