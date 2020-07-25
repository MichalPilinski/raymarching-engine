using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace raymarching.Static
{
    static class ColorUtils
    {
        static public Color Multiply(Color Color, float Scalar)
        {
            int R = (int)(Color.R * Scalar);
            int G = (int)(Color.G * Scalar);
            int B = (int)(Color.B * Scalar);

            return LimitColor(R, G, B);
        }

        static public Color Add(Color Color1, Color Color2)
        {
            int R = Color1.R + Color2.R;
            int G = Color1.G + Color2.G;
            int B = Color1.B + Color2.B;

            return LimitColor(R, G, B);
        }
        static public Color Add(Color Color1, Color Color2, Color Color3)
        {
            int R = Color1.R + Color2.R + Color3.R;
            int G = Color1.G + Color2.G + Color3.R;
            int B = Color1.B + Color2.B + Color3.R;

            return LimitColor(R, G, B);
        }

        private static Color LimitColor(int R, int G, int B)
        {
            R = (R > 255 ? 255 : R);
            G = (G > 255 ? 255 : G);
            B = (B > 255 ? 255 : B);

            return Color.FromArgb(0, R, G, B);
        }
    }
}
