using GitBay3.Common;
using System;

namespace GitBay3.Server.Data
{
    public class DataProvider : IDataProvider
    {
        private IMarketPlace _market;
        private Account _account;

        public DataProvider() : this(MarketPlace.Instance)
        {

        }

        public DataProvider(IMarketPlace marketPlace)
        {
            _market = marketPlace;
        }
        
        public AccountEntity LoadAccount(string accountName)
        {
            _account = new Account(accountName);
            return _account;
        }

        public float DelegatePurchaseOperation(Currencies name, float value)
        {
            var rate = _market.GetRate(name);
            var cost = rate * value;
            _account.ChangeBalance(name, value);
            _account.ChangeBalance(0, -cost);
            return cost;
        }

        public float DelegateSaleOperation(Currencies name, float value)
        {
            var rate = _market.GetRate(name);
            var cost = rate * value;
            _account.ChangeBalance(name, -value);
            _account.ChangeBalance(0, cost);
            return cost;
        }        
    }
}
