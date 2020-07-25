using raymarching.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.Abstractions
{
    class LightBase: ILight
    {
        public Vector3 Position { get; private set; }
        public Color Color { get; private set; }
        public float Intensity { get; private set; }

        public LightBase (Vector3 _position, float _intensity)
        {
            Position = _position;
            Intensity = _intensity;
        }

        public void SetColor(Color _color)
        {
            Color = _color;
        }
    }
}
