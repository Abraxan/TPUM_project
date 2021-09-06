using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Server.Api
{
    public interface IDataEndpoint
    {
        public bool Init();
        public void Dispose();
    }
}
