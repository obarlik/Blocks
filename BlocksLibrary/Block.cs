using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    [Serializable]
    public class Block
    {
        public Block()
        {
            Width = 100;
            Height = 100;
            Sockets = new Socket[2];
            Plugs = new Plug[3];
        }

        public string Name { get; set; }

        public string Description { get; protected set; }

        public ushort Left { get; set; }
        public ushort Top { get; set; }

        public ushort Width { get; protected set; }
        public ushort Height { get; protected set; }

        public Socket[] Sockets { get; protected set; }

        public Plug[] Plugs { get; protected set; }


        public virtual Bitmap Render()
        {
            var bmp = new Bitmap(Width, Height);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawButton(0, 0, Width, Height);

                for (var i = 0; i < Sockets.Length; i++)
                {
                    gr.DrawEllipse(Pens.Lime, 3, 3 + 4 * i, 2, 2);
                }

                for (var i = 0; i < Plugs.Length; i++)
                {
                    gr.DrawEllipse(Pens.Orange, Width - 5, 3 + 4 * i, 2, 2);
                }
            }

            return bmp;
        }


        class SocketSignal
        {
            public Socket Socket { get; set; }
            public Signal Signal { get; set; }
        }


        Queue<SocketSignal> Buffer = new Queue<SocketSignal>();

        AutoResetEvent BufferAvailable = new AutoResetEvent(false);

        Task ProcessTask { get; set; }


        public virtual bool AcceptsPlug(Socket socket, Plug plug)
        {
            return true;    
        }


        public void ReceiveSignal(Socket socket, Signal signal)
        {
            lock (Buffer)
            {
                Buffer.Enqueue(new SocketSignal()
                {
                    Socket = socket,
                    Signal = signal
                });

                socket.Status = SocketStatus.Ready;
                BufferAvailable.Set();

                if (ProcessTask != null)
                    return;

                ProcessTask = Task.Run(() =>
                {
                    try
                    {
                        while (BufferAvailable.WaitOne(10))
                        {
                            SocketSignal newSignal;

                            lock (Buffer)
                            {
                                newSignal = Buffer.Dequeue();
                            }

                            ProcessSignal(
                                newSignal.Socket,
                                newSignal.Signal);
                        }
                    }
                    finally
                    {
                        ProcessTask = null;
                    }
                });
            }
        }

        
        protected virtual void ProcessSignal(Socket socket, Signal signal)
        {
        }
    }
}
