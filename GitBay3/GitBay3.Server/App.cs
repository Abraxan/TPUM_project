using GitBay3.Common;
using GitBay3.Server.Logic;
using System;

namespace GitBay3.Server
{
    public class App : IDisposable
    {
        private IDataEndpoint _dataEndpoint;
        private IMarketEndpoint _marketEndpoint;

        public App() : this(new DataEndpoint(), new MarketEndpoint())
        {

        }

        public App(IDataEndpoint dataEndpoint, IMarketEndpoint marketEndpoint)
        {
            _dataEndpoint = dataEndpoint;
            _marketEndpoint = marketEndpoint;
        }

        public void Dispose()
        {
            _marketEndpoint.Dispose();
            _dataEndpoint.Dispose();
        }

        public void Init()
        {
            _dataEndpoint.Init((uri) => _marketEndpoint.SetUri(uri));
            _marketEndpoint.Init();
        }
    }
}
