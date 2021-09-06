using GitBay3.Common;
using GitBay3.Server.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GitBay3.Server.Logic.Tests
{
    [TestClass]
    public class LogicHandlerTests
    {
        [TestMethod]
        public void ProcessPurchaseRequest_Success()
        {
            //prepare
            Mock<IDataProvider> dataProvider = new Mock<IDataProvider>();
            dataProvider.Setup(x => x.DelegatePurchaseOperation(It.IsAny<Currencies>(), It.IsAny<float>()))
                .Returns((Currencies c, float x) => x + 10);
            dataProvider.Setup(x => x.LoadAccount(It.IsAny<string>()))
                .Returns((string s) => new Account(s));
            DataLogicHandler logicHandler = new DataLogicHandler(dataProvider.Object);
            logicHandler.Init();
            //execute
            float result = logicHandler.ProcessPurchaseRequest(Currencies.BTC, 15);
            //assert
            Assert.AreEqual(25, result);
            //dataProvider.Verify(x => x.DelegatePurchaseOperation(It.IsAny<Currencies>(), It.IsAny<float>()), Times.Once);
        }
    }
}
