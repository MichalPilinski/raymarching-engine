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

        public Color GetColor(Vector3 LightingCoefs, Vector3 CameraPos, Vector3 IntersectPos, List<ILight> Lights)
        {
            var Ambient = GetAmbient(LightingCoefs.X);

            var Diffuse = Color.Black;
            var Specular = Color.Black;

            foreach (var Light in Lights)
            {
                Diffuse = ColorUtils.Add(Diffuse, GetDiffused(CameraPos, IntersectPos, Light, LightingCoefs.Y));
            }

            return ColorUtils.Add(Ambient, Diffuse, Specular);
        }

        Color GetAmbient(float AmbientCoef)
        {
            return ColorUtils.Multiply(AmbientColor, AmbientCoef);
        }

        Color GetDiffused(Vector3 CameraPos, Vector3 IntersectPos, ILight Light, float LightingCoef)
        {
            Vector3 VecIncoming = CameraPos - IntersectPos;
            Vector3 VecOutcoming = Light.Position - IntersectPos;
            
            float AngleCos = Vector3.Dot(VecIncoming, VecOutcoming) / (VecIncoming.Length() * VecOutcoming.Length());

            return ColorUtils.Multiply(Light.Color, AngleCos * Light.Intensity * LightingCoef);
        }
    }
}
