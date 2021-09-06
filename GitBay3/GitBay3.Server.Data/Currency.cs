using GitBay3.Common;

namespace GitBay3.Server.Data
{
    public class Currency : CurrencyEntity
    {
        public Currency(Currencies name, float price)
        {
            Name = name;
            Price = price;
        }
        
        public override void ChangePrice(float newValue)
        {
            _price = newValue;
        }
    }
}
