using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace GitBay3.Server.Api
{
    public class Api : IApi, IMarketStateObserver
    {
        public async Task Begin(int p2p_port, Action<WebSocketConnection> onConnection)
        {
            Uri _uri = new Uri($@"http://localhost:{p2p_port}/");
            await ApiLoop(_uri, onConnection);
        }

        public void StateUpdate(Dictionary<Currencies, float> updatedRates)
        {
            throw new NotImplementedException();
        }

        private async Task ApiLoop(Uri _uri, Action<WebSocketConnection> onConnection)
        {
            HttpListener _server = new HttpListener();
            _server.Prefixes.Add(_uri.ToString());
            _server.Start();
            while (true)
            {
                HttpListenerContext _hc = await _server.GetContextAsync();
                if (!_hc.Request.IsWebSocketRequest)
                {
                    _hc.Response.StatusCode = 400;
                    _hc.Response.Close();
                }
                HttpListenerWebSocketContext _context = await _hc.AcceptWebSocketAsync(null);
                WebSocketConnection ws = new ServerWebSocketConnection(_context.WebSocket, _hc.Request.RemoteEndPoint);
                onConnection?.Invoke(ws);
            }
        }
    }
}
