using GitBay3.Common;
using GitBay3.Server.Data;
using System;
using System.Collections.Generic;

namespace GitBay3.Server.Logic
{
    public class DataLogicHandler : IDataLogicHandler
    {
        private IDataProvider _dataProvider;
        private AccountEntity _account;

        public DataLogicHandler() : this(new DataProvider())
        {
            
        }

        public DataLogicHandler(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Init()
        {
            _account = _dataProvider.LoadAccount("Test Testowy");
        }

        public float ProcessPurchaseRequest(Currencies name, float value)
        {
            float result;
            try
            {
                result = _dataProvider.DelegatePurchaseOperation(name, value);
                _account.Wallet[name] += result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public float ProcessSaleRequest(Currencies name, float value)
        {
            float result;
            try
            {
                result = _dataProvider.DelegateSaleOperation(name, value);
                _account.Wallet[name] -= result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Dictionary<Currencies, float> GetWallet()
        {
            return _account.Wallet;
        }
    }
}
