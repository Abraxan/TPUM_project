using GitBay3.Common;
using GitBay3.Server.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server
{
    public class DataEndpoint : IDataEndpoint
    {
        private WebSocketConnection _mServer = null;
        private List<string> _logOutput;
        private Action<string> _log;
        private Uri _uri;
        private IDataLogicHandler _handler;
        private Action<string> _uriReceivedAction = null;

        public DataEndpoint() : this(new DataLogicHandler())
        {

        }

        public DataEndpoint(IDataLogicHandler handler)
        {
            _logOutput = new List<string>();
            _log = (message) =>
            {
                var log = string.Format(@"At {0} received: {1}", DateTime.UtcNow.ToString("s"), message);
                _logOutput.Add(log);
                Console.WriteLine(log);
            };
            _uri = new Uri("ws://localhost:6966");
            _handler = handler;
        }

        public bool Init(Action<string> uriReceivedAction)
        {
            _uriReceivedAction = uriReceivedAction;
            _handler.Init();
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
                            switch (message.Type)
                            {
                                case (MessageTypes.PurchaiseConfirm):
                                    {
                                        HandleRequest(message.Data, _handler.ProcessPurchaseRequest);
                                        break;
                                    }
                                case (MessageTypes.SellConfirm):
                                    {
                                        HandleRequest(message.Data, _handler.ProcessSaleRequest);
                                        break;
                                    }
                                case (MessageTypes.Setup):
                                    {
                                        _uriReceivedAction(message.Text);
                                        break;
                                    }
                                case (MessageTypes.WalletState):
                                    {
                                        HandleWalletRequest();
                                        break;
                                    }
                            }
                        };
                    }));
                return true;
            }
            catch (Exception ex)
            {
                _log(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            _mServer?.DisconnectAsync();
        }

        private void HandleRequest(List<Entry> data, Func<Currencies, float, float> handler)
        {
            var entry = data.First();
            var result = handler(entry.Currency, entry.Value);
            var resultEntry = new Entry()
            {
                Currency = Currencies.PLN,
                Value = result
            };
            var resultSet = new List<Entry>();
            resultSet.Add(resultEntry);
            var resultMessage = new Message()
            {
                Type = MessageTypes.PurchaiseConfirm,
                Data = resultSet
            };
            Task.Run(async () =>
            {
                await _mServer.SendAsync(DataConverter.Serialize(resultMessage));
                _log("Sent purchaise confirmation to client: " + result);
            });
            
        }

        private void HandleWalletRequest()
        {
            var result = _handler.GetWallet();
            var resultMessage = new Message()
            {
                Type = MessageTypes.WalletState,
                Data = DataConverter.Convert(result)
            };
            Task.Run(async () =>
            {
                await _mServer.SendAsync(DataConverter.Serialize(resultMessage));
                _log("Sent wallet update to client: " + result);
            });
        }
    }
}
