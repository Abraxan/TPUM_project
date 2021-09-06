using GitBay3.Common;

namespace GitBay3.Server.Common
{
    public interface ILogicHandler
    {
        float ProcessPurchaseRequest(Currencies name, float value);
        float ProcessSaleRequest(Currencies name, float value);
    }
}
