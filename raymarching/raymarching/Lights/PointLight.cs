using raymarching.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.Lights
{
    class PointLight: LightBase
    {
        public PointLight(Vector3 Position, float Intensity): base(Position, Intensity)
        {

        }
    }
}
