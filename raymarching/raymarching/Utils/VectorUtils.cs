using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raymarching.Static
{
    static class VectorUtils
    {
        public static Vector3 ScaleToUnity(Vector3 Vector)
        {
            return Vector / Math.Max(Vector.X, Math.Max(Vector.Y, Vector.Z));
        }
    }
}
