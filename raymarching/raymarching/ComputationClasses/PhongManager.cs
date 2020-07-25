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
        public PhongManager(){}

        public Color GetColor(List<ILight> Lights, Color ObjectColor, Vector3 LightingCoefs, Vector3 CameraPos, Vector3 IntersectPos, Vector3 Normal)
        {
            var Ambient = GetAmbient(ObjectColor, LightingCoefs.X);

            var Diffuse = Color.Black;
            var Specular = Color.Black;

            foreach (var Light in Lights)
            {
                Diffuse = ColorUtils.Add(Diffuse, GetDiffused(Normal, IntersectPos, Light, LightingCoefs.Y));
                Specular = ColorUtils.Add(Specular, GetSpecular(Normal, IntersectPos, CameraPos, Light, LightingCoefs.Z));
            }

            return ColorUtils.Add(Ambient, Diffuse, Specular);
        }

        Color GetAmbient(Color ObjectColor, float AmbientCoef)
        {
            return ColorUtils.Multiply(ObjectColor, AmbientCoef);
        }

        Color GetDiffused(Vector3 Normal, Vector3 IntersectPos, ILight Light, float LightingCoef)
        {
            Vector3 VecOutcoming = Light.Position - IntersectPos;

            float AngleCos = GetVectorAngle(Normal, VecOutcoming);
            
            return ColorUtils.Multiply(Light.Color, AngleCos * Light.Intensity * LightingCoef);
        }

        Color GetSpecular(Vector3 Normal, Vector3 IntersectPos, Vector3 CameraPos, ILight Light, float LightingCoef)
        {
            Vector3 VecComing = IntersectPos - Light.Position;
            Normal = Vector3.Normalize(Normal);

            Vector3 ReflectedVector = VecComing - 2 * Vector3.Dot(VecComing, Normal) * Normal;
            Vector3 IntersectToCamera = CameraPos - IntersectPos;

            float AngleCos = GetVectorAngle(IntersectToCamera, ReflectedVector);
            float SpecularMultiplier = (float)Math.Pow((double)AngleCos, (double)LightingCoef) * Light.Intensity;

            return ColorUtils.Multiply(Light.Color, SpecularMultiplier);
        }

        private float GetVectorAngle(Vector3 VecOne, Vector3 VecSec)
        {
            float AngleCos = Vector3.Dot(VecOne, VecSec) / (VecOne.Length() * VecSec.Length());

            return AngleCos < 0 ? 0 : AngleCos;
        }
    }
}
