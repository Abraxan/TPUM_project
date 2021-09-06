using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Api
{
    public interface IApi
    {
        public Task Begin(int p2p_port, Action<WebSocketConnection> onConnection);

    }
}
