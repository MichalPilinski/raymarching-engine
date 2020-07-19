using raymarching.Interfaces;
using raymarching.Static;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace raymarching.ComputationClasses
{
    class PhongManager
    {

        private Color AmbientColor;

        public PhongManager(Color _ambient)
        {
            AmbientColor = _ambient;
        }

        public Color GetColor(Vector3 LightingCoefs, Vector3 ObjectPos, List<ILight> Lights)
        {
            var Ambient = GetAmbient(LightingCoefs.X);

            var Diffuse = Color.Black;
            var Specular = Color.Black;

            foreach (var Light in Lights)
            {
                Diffuse = ColorUtils.Add(Diffuse, GetDiffused(LightingCoefs.Y, Light, ObjectPos));
            }

            return ColorUtils.Add(Ambient, Diffuse, Specular);
        }

        Color GetAmbient(float AmbientCoef)
        {
            return ColorUtils.Multiply(AmbientColor, AmbientCoef);
        }

        Color GetDiffused(float LightingCoef, ILight Light, Vector3 ObjectPos)
        {
            return Color.Black;
        }
    }
}
