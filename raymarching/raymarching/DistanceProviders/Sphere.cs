using raymarching.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.DistanceProviders
{
    class Sphere: DistanceProviderBase
    {
        public Sphere(Vector3 _position, float _radius, Vector3 _lighting) : base(_position, _lighting)
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
