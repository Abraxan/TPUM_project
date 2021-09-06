using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Client.Data
{
    public interface IMarketProvider
    {
        public bool Init();
        public void Dispose();
        public string GetUri();
    }
}
