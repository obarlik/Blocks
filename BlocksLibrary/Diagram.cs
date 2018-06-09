using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    [Serializable]
    public class Diagram
    {
        public Diagram()
        {
            Blocks = new List<Block>();
        }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public List<Block> Blocks { get; protected set; }
    }
}
