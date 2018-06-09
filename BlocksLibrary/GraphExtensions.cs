
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocksLibrary
{
    public static class GraphExtensions
    {
        public static void DrawButton(this Graphics gr, int left, int top, int width, int height)
        {
            var p1 = new Point(left, top);
            var p2 = new Point(width, height);

            var cGroup = new[]
            {
                    new[]{ Pens.White, Pens.Black },
                    new[]{ Pens.LightGray, Pens.DarkGray }
                };

            foreach (var cg in cGroup)
            {
                gr.DrawLine(cg[0], p1.X, p1.Y, p2.X, p1.Y);
                gr.DrawLine(cg[0], p1.X, p1.Y, p1.X, p2.Y);

                gr.DrawLine(cg[1], p2.X, p1.Y, p2.X, p2.Y);
                gr.DrawLine(cg[1], p1.X, p2.Y, p2.X, p2.Y);

                p1.Offset(1, 1);
                p2.Offset(-1, -1);
            }

            gr.DrawRectangle(Pens.Gray, p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
