using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    [Serializable]
    public class Plug
    {
        public Plug()
        {
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }
    }
}
