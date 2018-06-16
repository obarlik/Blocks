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
    public class Block : IDisposable
    {
        public Block()
        {
            Name = "Generic Block";
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
            var sz = 15;
            var gap = 10;

            var bmp = new Bitmap(Width, Height);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, Width, Height);

                var y = (Height - Sockets.Length * sz - (Sockets.Length - 1) * gap) / 2;

                for (var i = 0; i < Sockets.Length; i++)
                {
                    gr.DrawButton(0, y + (sz + gap) * i, sz, sz / 2);
                }

                y = (Height - Plugs.Length * sz - (Plugs.Length - 1) * gap) / 2;

                for (var i = 0; i < Plugs.Length; i++)
                {
                    gr.DrawButton(Width - sz, y + (sz + gap) * i, sz, sz / 2);
                }

                gr.DrawButton(sz - 2, 0, Width - 2 * sz + 4, Height);

                //var cx = Width / 2;
                //var cy = Height / 2;
                var fnt = SystemFonts.CaptionFont;
                var rt = new RectangleF(sz + 2, 2, Width - 2 * sz - 4, Height - 4);

                gr.DrawString(
                    Name, fnt,
                    Brushes.Black,
                    rt,
                    new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
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
                        while (!Disposed && BufferAvailable.WaitOne(10))
                        {
                            SocketSignal newSignal = null;

                            lock (Buffer)
                            {
                                if (!Disposed)
                                    newSignal = Buffer.Dequeue();
                            }

                            if (!Disposed && newSignal != null)
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


        bool Disposed;

        public void Dispose()
        {
            if (Disposed)
                return;

            Disposed = true;

            lock (Buffer)
            {
                Buffer.Clear();
                BufferAvailable.Reset();
            }
        }
    }
}
