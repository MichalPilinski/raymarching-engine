using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.Interfaces
{
    interface IDistanceProvider
    {
        float GetDistance(Vector3 rayPosition);
        Vector3 LightingCoefs { get; }
        Color Color { get; }
        Vector3 GetGradient(Vector3 ProbedPlace, float Epsilon);
    }
}
