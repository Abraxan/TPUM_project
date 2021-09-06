using GitBay3.Common;

namespace GitBay3.Server.Common
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
