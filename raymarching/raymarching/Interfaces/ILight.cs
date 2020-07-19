using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.Interfaces
{
    interface ILight
    {
        void SetColor(Color _color);
        public Vector3 Position { get; }
        public Color Color { get; }
        public float Intensity { get; }
    }
}
