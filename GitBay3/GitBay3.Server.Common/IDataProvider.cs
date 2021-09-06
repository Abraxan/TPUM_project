using GitBay3.Common;

namespace GitBay3.Server.Common
{
    public interface IDataProvider
    {
        AccountEntity LoadAccount(string accountName);
        float DelegatePurchaseOperation(Currencies name, float value);
        float DelegateSaleOperation(Currencies name, float value);
    }
}
