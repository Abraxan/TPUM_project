using GitBay3.Common;
using System;
using System.Threading.Tasks;

namespace GitBay3.Server.Common
{
    public interface IApi
    {
        public Task Begin(int p2p_port, Action<WebSocketConnection> onConnection);

    }
}
