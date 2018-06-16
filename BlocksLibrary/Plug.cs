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

        public List<Socket> PluggedTo { get; protected set; }


        public void SendSignal(Signal signal)
        {
            PluggedTo
            .AsParallel()
            .ForAll(socket => socket.ReceiveSignal(signal));
        }


        public void PlugTo(Socket socket)
        {
            if (socket.AcceptsPlug(this))
            {
                if (PluggedTo == null)
                    PluggedTo = new List<Socket>();

                PluggedTo.Add(socket);
            }
        }
    }
}
