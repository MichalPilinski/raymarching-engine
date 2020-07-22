using raymarching.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.DistanceProviders
{
    class Sphere: DistanceProviderBase
    {
        public Sphere(Vector3 _position, float _radius, Vector3 _lightingCoefs, Color _color) : base(_position, _lightingCoefs, _color)
        {
            Radius = _radius;
        }

        private float Radius;

        public override float GetDistance(Vector3 RayPosition)
        {
            return base.GetDistance(RayPosition) - Radius;
        }
    }
}
