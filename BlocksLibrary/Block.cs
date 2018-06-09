using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    [Serializable]
    public class Block
    {
        public Block()
        {

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
    }
}
