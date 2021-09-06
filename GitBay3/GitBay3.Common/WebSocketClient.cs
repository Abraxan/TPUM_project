using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public class WebSocketClient
    {
        public static async Task<WebSocketConnection> Connect(Uri peer, Action<string> log)
        {
            ClientWebSocket m_ClientWebSocket = new ClientWebSocket();
            await m_ClientWebSocket.ConnectAsync(peer, CancellationToken.None);
            switch (m_ClientWebSocket.State)
            {
                case WebSocketState.Open:
                    log($"Opening WebSocket connection to remote server {peer}");
                    WebSocketConnection _socket = new ClientWebSocketConnection(m_ClientWebSocket, peer, log);
                    return _socket;

                default:
                    log?.Invoke($"Cannot connect to remote node status {m_ClientWebSocket.State}");
                    throw new WebSocketException($"Cannot connect to remote node status {m_ClientWebSocket.State}");
            }
        }
    }
}
