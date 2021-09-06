using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Data
{
    public class DataProvider : IDataProvider
    {
        private WebSocketConnection _dClient = null;
        private List<string> _logOutput;
        private Action<string> _log;
        private Uri _uri;

        public DataProvider()
        {
            _logOutput = new List<string>();
            _log = (message) =>
            {
                var log = string.Format(@"At {0} received: {1}", DateTime.UtcNow.ToString("s"), message);
                _logOutput.Add(log);
                Console.WriteLine(log);
            };
            _uri = new Uri("ws://localhost:6966");
        }

        public float Buy(Currencies name, float amount)
        {
            var entry = new Entry()
            {
                Currency = name,
                Value = amount
            };
            var result = HandleRequest(entry, MessageTypes.PurchaiseConfirm);
            return result;
        }

        public float Sell(Currencies name, float amount)
        {
            var entry = new Entry()
            {
                Currency = name,
                Value = amount
            };
            var result = HandleRequest(entry, MessageTypes.SellConfirm);
            return result;
        }

        public Dictionary<Currencies, float> GetWallet()
        {
            var result = HandleWalletRequest();
            return result;
        }

        public bool Init(string uri)
        {
            try
            {
                _dClient = Task.Run(async () => await WebSocketClient.Connect(_uri, _log)).Result;
                Task send = Task.Run(async () =>
                {
                    await _dClient.SendAsync(DataConverter.Serialize(new Message()
                    {
                        Type = MessageTypes.Setup,
                        Text = uri
                    }));
                    _log("Send massage to server: " + uri);
                });
                send.Wait();
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
            _dClient?.DisconnectAsync();
        }

        private float HandleRequest(Entry entry, MessageTypes type)
        {
            var requestSet = new List<Entry>();
            requestSet.Add(entry);
            var requestMessage = new Message()
            {
                Type = type,
                Data = requestSet
            };
            var result = 0.0f;
            Task<float> receive = null;
            _dClient.onMessage = (data) =>
            {
                receive = Task.Run(() =>
               {
                   var message = DataConverter.Deserialize<Message>(data);
                   result = message.Data.First().Value;
                   return result;
               });
                result = receive.Result;
            };
            Task send = Task.Run(async () =>
            {
                await _dClient.SendAsync(DataConverter.Serialize(requestMessage));
                _log("Sent request to server: " + type);
            });
            send.Wait();
            while (receive == null)
            {
                Task.Delay(10);
            }
            receive.Wait();
            return result;
        }

        private Dictionary<Currencies, float> HandleWalletRequest()
        {
            var result = new Dictionary<Currencies, float>();
            
            var requestMessage = new Message()
            {
                Type = MessageTypes.WalletState
            };
            Task<Dictionary<Currencies, float>> receive = null;
            _dClient.onMessage = (data) =>
            {
                receive = Task.Run(() =>
                {
                    var message = DataConverter.Deserialize<Message>(data);
                    result = DataConverter.Convert(message.Data);
                    return result;
                });
                result = receive.Result;
            };

            Task send = Task.Run(async () =>
            {
                await _dClient.SendAsync(DataConverter.Serialize(requestMessage));
                _log("Sent request to server: " + requestMessage.Type);
            });
            send.Wait();
            while (receive == null)
            {
                Task.Delay(10);
            }
            receive.Wait();

            return result;
        }
    }
}
