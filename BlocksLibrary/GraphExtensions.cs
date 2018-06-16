
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
        public static void DrawCircle(this Graphics gr, int cx, int cy, int r, Color c)
        {
            gr.DrawEllipse(
                 new Pen(c),
                 cx - r,
                 cy - r,
                 2 * r,
                 2 * r);
        }


        public static void DrawDisc(this Graphics gr, int cx, int cy, int r, Color c)
        {
            gr.FillEllipse(
                 new SolidBrush(c),
                 cx - r,
                 cy - r,
                 2 * r - 1,
                 2 * r - 1);
        }


        public static void DrawButton(this Graphics gr, int left, int top, int width, int height, bool up = true)
        {
            var p1 = new Point(left, top);
            var p2 = new Point(left + width - 1, top + height - 1);

            var cGroup = new[]
            {
                new[]{ Pens.White, Pens.Black },
                new[]{ Pens.LightGray, Pens.DarkGray },
                new[]{ Pens.DarkGray, Pens.LightGray },
                new[]{ Pens.Black, Pens.White },
            };

            for (var cgi = 0; cgi < 2; cgi++)
            {
                var cg = cGroup[cgi + (up ? 0 : 2)];

                gr.DrawLine(cg[0], p1.X, p1.Y, p2.X, p1.Y);
                gr.DrawLine(cg[0], p1.X, p1.Y, p1.X, p2.Y);

                gr.DrawLine(cg[1], p2.X, p1.Y, p2.X, p2.Y);
                gr.DrawLine(cg[1], p1.X, p2.Y, p2.X, p2.Y);

                p1.Offset(1, 1);
                p2.Offset(-1, -1);
            }

            gr.FillRectangle(Brushes.LightGray, p1.X, p1.Y, p2.X - 1 - left, p2.Y - 1 - top);
        }
    }
}
