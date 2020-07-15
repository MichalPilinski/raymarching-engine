using raymarching.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.Abstractions
{
    class DistanceProviderBase: IDistanceProvider
    {
        Vector3 Position { get; set; }

        protected DistanceProviderBase(Vector3 _position)
        {
            Position = _position;
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
