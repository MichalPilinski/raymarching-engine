using raymarching.Interfaces;
using raymarching.Static;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.Abstractions
{
    class DistanceProviderBase: IDistanceProvider
    {
        Vector3 Position { get; set; }
        public Vector3 LightingCoefs { get; private set; } // ambient, diffuse, specular

        protected DistanceProviderBase(Vector3 _position, Vector3 _lighting)
        {
            Position = _position;
            LightingCoefs = VectorUtils.ScaleToUnity(_lighting);
        }

        public virtual float GetDistance(Vector3 RayPosition)
        {
            return Vector3.Distance(Position, RayPosition);
        }

        public virtual Vector3 GetNormal(Vector3 RayPosition, float RayLength)
        {
            return new Vector3(); // TODO
        }
    }
}
