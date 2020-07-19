using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.Interfaces
{
    interface IDistanceProvider
    {
        float GetDistance(Vector3 rayPosition);
        Vector3 LightingCoefs { get; }
    }
}
