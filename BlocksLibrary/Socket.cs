using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    [Serializable]
    public class Socket
    {
        public Socket()
        {

        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }
    }
}
