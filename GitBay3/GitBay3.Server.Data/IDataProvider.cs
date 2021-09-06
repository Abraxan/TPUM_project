using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Data
{
    public interface IDataProvider
    {
        AccountEntity LoadAccount(string accountName);
        float DelegatePurchaseOperation(Currencies name, float value);
        float DelegateSaleOperation(Currencies name, float value);
    }
}
