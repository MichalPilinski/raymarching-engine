using raymarching.Interfaces;
using raymarching.Static;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.Abstractions
{
    class DistanceProviderBase : IDistanceProvider
    {
        Vector3 Position { get; set; }
        public Vector3 LightingCoefs { get; private set; } // ambient, diffuse, specular
        public Color Color { get; private set; }

        protected DistanceProviderBase(Vector3 _position, Vector3 _lighting, Color _color)
        {
            Position = _position;
            LightingCoefs = _lighting;
            Color = _color;
        }

        public virtual float GetDistance(Vector3 RayPosition)
        {
            return Vector3.Distance(Position, RayPosition);
        }

        public virtual Vector3 GetNormal(Vector3 RayPosition, float RayLength)
        {
            return new Vector3(); // TODO
        }

        public virtual Vector3 GetGradient(Vector3 ProbedPlace, float Epsilon)
        {
            float ProbedPlaceDistance = this.GetDistance(ProbedPlace);

            float XDistance = this.GetDistance(ProbedPlace + new Vector3(Epsilon, 0, 0)) - ProbedPlaceDistance;
            float YDistance = this.GetDistance(ProbedPlace + new Vector3(0, Epsilon, 0)) - ProbedPlaceDistance;
            float ZDistance = this.GetDistance(ProbedPlace + new Vector3(0, 0, Epsilon)) - ProbedPlaceDistance;

            return new Vector3(XDistance, YDistance, ZDistance) / Epsilon;
        }
    }
}
