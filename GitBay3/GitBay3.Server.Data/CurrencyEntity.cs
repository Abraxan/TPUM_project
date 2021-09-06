using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Data
{
    public abstract class CurrencyEntity
    {
        private Currencies _name;
        protected float _price;

        public Currencies Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public float Price
        {
            get { return _price; }
            protected set { _price = value; }
        }

        public abstract void ChangePrice(float newValue);
    }
}
