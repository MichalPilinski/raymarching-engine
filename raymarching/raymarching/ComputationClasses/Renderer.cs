using raymarching.Abstractions;
using raymarching.DistanceProviders;
using raymarching.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.ComputationClasses
{
    class Renderer
    {
        private List<IDistanceProvider> DistanceObjects;

        private Vector2 WindowSize;
        private Vector2 RayBoundaryLength;

        private Camera Camera;

        public Renderer(Vector2 _windowSize, Vector2 _distanceBoundary)
        {
            DistanceObjects = new List<IDistanceProvider>();

            WindowSize = _windowSize;
            RayBoundaryLength = _distanceBoundary;

            InitializeCamera();

            DistanceObjects.Add(new Sphere(new Vector3(5, 5, 5), 2));
        }

        private void InitializeCamera()
        {
            Camera = new Camera(new Vector2(1, 1), WindowSize, 1f, 0);
            Camera.PointAt(new Vector3(5, 5, 5));
            Camera.Place(new Vector3(0, 0, 0));

            Camera.Update();
        }

        public Color[,] Render()
        {
            Color[,] Pixels = new Color[(int)WindowSize.X, (int)WindowSize.Y];
            Vector3 LocalRayDirection;

            for (int i = 0; i < WindowSize.X; i++)
            {
                for(int j = 0; j < WindowSize.Y; j++)
                {
                    LocalRayDirection = Camera.GetRayDirection(i, j);
                    ComputeRay(Camera.CameraPosition, LocalRayDirection);
                }
            }

            return Pixels;
        }

        private Color ComputeRay(Vector3 RayStartingPos, Vector3 RayDirection)
        {
            float FailSafeCount = 1000;
            Vector3 RayPosition = RayStartingPos;

            float Distance = GetMinimumDistance(RayPosition);

            while (Distance > RayBoundaryLength.X && Distance < RayBoundaryLength.Y)
            {
                FailSafeCount--;
                if(FailSafeCount < 0)
                {
                    break;
                }

                Distance = GetMinimumDistance(RayPosition);
                RayPosition += RayDirection * Distance;
            }

            return Distance < RayBoundaryLength.X ? Color.White : Color.Black;
        }

        private float GetMinimumDistance(Vector3 Position)
        {
            float MinDistance = float.PositiveInfinity;

            float CurrentDistance = 0;
            foreach(var DistanceObject in DistanceObjects)
            {
                CurrentDistance = DistanceObject.GetDistance(Position);
                if (CurrentDistance < MinDistance)
                {
                    MinDistance = CurrentDistance;
                }
            }

            return MinDistance;
        }
    }
}
