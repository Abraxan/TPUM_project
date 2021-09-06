using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server
{
    public interface IMarketEndpoint
    {
        public void SetUri(string uri);
        public void Init();
        public void Dispose();
    }
}
