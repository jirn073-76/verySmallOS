using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.Debug.Kernel;
using Point = Cosmos.System.Graphics.Point;

namespace VerySmallOS
{
    static class GraphicsSubsystemUtil
    {
        // (Scuffed) Back buffer to stop the screen from flickering
        public static byte[] SBuffer = new byte[64000];
        public static Pen pen = new Pen(Color.Red, 60);
        public static bool IsGraphicsSubsystemRunning { get; private set; } = false;
        public static bool IsCursorEnabled { get; private set; } = false;
        static Canvas canvas;
        static Color color;

        public static void StartGraphicalMode(Color color)
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            GraphicsSubsystemUtil.color = color;
            canvas.Clear(color);
            IsGraphicsSubsystemRunning = true;
        }

        public static void EnableCursor()
        {
            Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
            Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;
            IsCursorEnabled = true;
        }

        //Because who needs jagged arrays anyways?
        public static void SetPixel(int x, int y, int color)
            => SBuffer[(y * 320) + x] = (byte)color;


/*        public static void BufferedRedraw()
        {
            int c = 0;

            for (int y = 0; y < 200; y++)
            {
                for (int x = 0; x < 320; x++)
                {
                    uint cl = (uint)new Random().Next(0,255);
                    if (cl != (uint)SBuffer[c])
                    {
                        pen = new Pen(Color.FromArgb(SBuffer[c]), 40);
                        canvas.DrawPoint(pen, x, y);
                    }

                    c++;
                }
            }

            for (int i = 0; i < 64000; i++)
                SBuffer[i] = 0;

            Redraw();
        }
*/

        private static void DrawCursor(int X, int Y)
        {
            canvas.DrawPoint(pen, X, Y);
            canvas.DrawPoint(pen, X + 1, Y);
            canvas.DrawPoint(pen, X + 2, Y);
            canvas.DrawPoint(pen, X + 3, Y);
            canvas.DrawPoint(pen, X + 4, Y);
            canvas.DrawPoint(pen, X + 5, Y);
            canvas.DrawPoint(pen, X + 6, Y);
            canvas.DrawPoint(pen, X + 7, Y);
            canvas.DrawPoint(pen, X + 8, Y);
            canvas.DrawPoint(pen, X, Y + 1);
            canvas.DrawPoint(pen, X, Y + 2);
            canvas.DrawPoint(pen, X, Y + 3);
            canvas.DrawPoint(pen, X, Y + 4);
            canvas.DrawPoint(pen, X, Y + 5);
            canvas.DrawPoint(pen, X, Y + 6);
            canvas.DrawPoint(pen, X, Y + 7);
            canvas.DrawPoint(pen, X, Y + 8);
            canvas.DrawPoint(pen, X + 1, Y + 1);
            canvas.DrawPoint(pen, X + 2, Y + 2);
            canvas.DrawPoint(pen, X + 3, Y + 3);
            canvas.DrawPoint(pen, X + 4, Y + 4);
            canvas.DrawPoint(pen, X + 5, Y + 5);
            canvas.DrawPoint(pen, X + 6, Y + 6);
        }

        public static void Redraw()
        {
            canvas.Clear(color);
            pen = new Pen(Color.Red, 40);

            try
            {
                if (IsCursorEnabled)
                {
                    Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
                    Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;
                    int X = (int)Sys.MouseManager.X;
                    int Y = (int)Sys.MouseManager.Y;
                    DrawCursor(X, Y);
                }
                else
                {
                    canvas.DrawRectangle(pen, 450, 450, 80, 60);
                    canvas.DrawPoint(pen, 69, 69);

                    /* A GreenYellow horizontal line */
                    pen.Color = Color.GreenYellow;
                    canvas.DrawLine(pen, 250, 100, 400, 100);

                    /* An IndianRed vertical line */
                    pen.Color = Color.IndianRed;
                    canvas.DrawLine(pen, 350, 150, 350, 250);

                    /* A MintCream diagonal line */
                    pen.Color = Color.MintCream;
                    canvas.DrawLine(pen, 250, 150, 400, 250);

                    /* A PaleVioletRed rectangle */
                    pen.Color = Color.PaleVioletRed;
                    canvas.DrawRectangle(pen, 350, 350, 80, 60);

                    /* A LimeGreen rectangle */
                    pen.Color = Color.LimeGreen;
                    canvas.DrawRectangle(pen, 450, 450, 80, 60);
                }
            }
            catch (Exception ex)
            {
                Kernel.debugger.Send("Exception occurred: " + ex.Message);
                Kernel.debugger.Send(ex.Message);
            }

            canvas.Clear(color);
        }
    }
}
