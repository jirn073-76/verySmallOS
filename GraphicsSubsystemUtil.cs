using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using System.Drawing;

namespace VerySmallOS
{
    static class GraphicsSubsystemUtil
    {
        static Canvas canvas;
        public static void StartGraphicalMode()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();

            VGAScreen.SetGraphicsMode(VGAScreen.ScreenSize.Size320x200, VGAScreen.ColorDepth.BitDepth8);
            canvas.Clear(Color.Beige);
        }

        public static void EnableCursor()
        {

        }
    }
}
