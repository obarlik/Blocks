using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    public enum SocketStatus
    {
        Ready,
        Busy
    }


    [Serializable]
    public class Socket
    {
        public Socket()
        {
        }


        public Socket(Block block, string name, string description)
        {
        }


        public Block Block { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public SocketStatus Status { get; internal set; }


        public void ReceiveSignal(Signal signal)
        {
            Status = SocketStatus.Busy;
            Block.ReceiveSignal(this, signal);
        }
        

        public bool AcceptsPlug(Plug plug)
        {
            return Block.AcceptsPlug(this, plug);
        }
    }
}
