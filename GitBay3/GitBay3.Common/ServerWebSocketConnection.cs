using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public class ServerWebSocketConnection : WebSocketConnection
    {
        public ServerWebSocketConnection(WebSocket webSocket, IPEndPoint remoteEndPoint)
        {
            m_WebSocket = webSocket;
            m_remoteEndPoint = remoteEndPoint;
            Task.Factory.StartNew(() => ServerMessageLoop(webSocket));
        }

        protected override Task SendTask(string message)
        {
            return m_WebSocket.SendAsync(message.GetArraySegment(), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override Task DisconnectAsync()
        {
            return m_WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutdown procedure started", CancellationToken.None);
        }

        public override string ToString()
        {
            return m_remoteEndPoint.ToString();
        }

        private WebSocket m_WebSocket = null;
        private IPEndPoint m_remoteEndPoint;

        private void ServerMessageLoop(WebSocket ws)
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                ArraySegment<byte> _segments = new ArraySegment<byte>(buffer);
                WebSocketReceiveResult _receiveResult = ws.ReceiveAsync(_segments, CancellationToken.None).Result;
                if (_receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    onClose?.Invoke();
                    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None);
                    return;
                }
                int count = _receiveResult.Count;
                while (!_receiveResult.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        onClose?.Invoke();
                        ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                        return;
                    }
                    _segments = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    _receiveResult = ws.ReceiveAsync(_segments, CancellationToken.None).Result;
                    count += _receiveResult.Count;
                }
                string _message = Encoding.UTF8.GetString(buffer, 0, count);
                onMessage?.Invoke(_message);
            }
        }
    }
}
