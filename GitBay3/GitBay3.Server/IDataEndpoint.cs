using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server
{
    public interface IDataEndpoint
    {
        public bool Init(Action<string> uriReceivedAction);
        public void Dispose();
    }
}
