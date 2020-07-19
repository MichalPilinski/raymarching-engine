using raymarching.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.Lights
{
    class PointLight: LightBase
    {
        public PointLight(Vector3 Position, Color Color, float Intensity): base(Position, Intensity)
        {
            base.SetColor(Color);
        }
    }
}
