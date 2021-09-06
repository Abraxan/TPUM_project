using GitBay3.Common;
using GitBay3.Server.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server
{
    public class MarketEndpoint : IMarketEndpoint, IMarketStateObserver
    {
        private WebSocketConnection _mClient = null;
        private List<string> _logOutput;
        private Action<string> _log;
        private Uri _uri;
        private IMarketLogicHandler _handler;

        public MarketEndpoint() : this(new MarketLogicHandler())
        {

        }
        
        public MarketEndpoint(IMarketLogicHandler handler)
        {
            _handler = handler;
            _logOutput = new List<string>();
            _log = (message) =>
            {
                var log = string.Format(@"At {0} received: {1}", DateTime.UtcNow.ToString("s"), message);
                _logOutput.Add(log);
                Console.WriteLine(log);
            };
        }

        public void Init()
        {
            Task.Run(() =>
            {
                while (_uri == null)
                {
                    Task.Delay(250);
                }
                try
                {
                    _mClient = Task.Run(async () => await WebSocketClient.Connect(_uri, _log)).Result;
                    _handler.SubscribeToMarketPlace(this);
                    return true;
                }
                catch (Exception ex)
                {
                    _log(ex.Message);
                    return false;
                }
            });
        }

        public void Dispose()
        {
            _handler.UnsubscribeToMarketPlace(this);
            _mClient?.DisconnectAsync();
        }

        public void SetUri(string uri)
        {
            _uri = new Uri(uri);
        }

        public void StateUpdate(Dictionary<Currencies, float> updatedRates)
        {
            if (_mClient != null)
            {
                var resultSet = DataConverter.Convert(updatedRates);
                var resultMessage = new Message()
                {
                    Type = MessageTypes.MarketState,
                    Data = resultSet
                };
                Task.Run(async () =>
                {
                    await _mClient.SendAsync(DataConverter.Serialize(resultMessage));
                    _log("New rates sent to client: " + _uri.ToString());
                });
            }
        }
    }
}
