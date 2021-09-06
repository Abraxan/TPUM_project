using GitBay3.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Data
{
    public class MarketProvider : IMarketProvider
    {
        private WebSocketConnection _mServer = null;
        private List<string> _logOutput;
        private Action<string> _log;
        private Uri _uri;

        public MarketProvider()
        {
            _logOutput = new List<string>();
            _log = (message) =>
            {
                var log = string.Format(@"At {0} received: {1}", DateTime.UtcNow.ToString("s"), message);
                _logOutput.Add(log);
                Console.WriteLine(log);
            };
            _uri = new Uri("ws://localhost:6967");
        }

        public bool Init()
        {
            try
            {
                Task server = Task.Run(async () => await WebSocketServer.Server(_uri.Port,
                    _ws =>
                    {
                        _mServer = _ws;
                        _mServer.onMessage = (data) =>
                        {
                            _log($"Received message from client: { data}");
                            var message = DataConverter.Deserialize<Message>(data);
                            if (message.Type == MessageTypes.MarketState)
                            {
                                State.Instance.SetMarket(message.Data);
                                Task.Run(async () =>
                                {
                                    await _mServer.SendAsync("Received data.");
                                    _log($"Sent data received from client");
                                });
                            }
                        };
                    }));
                return true;
            }
            catch(Exception ex)
            {
                _log(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            _mServer?.DisconnectAsync();
        }

        public string GetUri()
        {
            return _uri.ToString();
        }
    }
}