using raymarching.Abstractions;
using raymarching.DistanceProviders;
using raymarching.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace raymarching.ComputationClasses
{
    class Renderer
    {
        private List<IDistanceProvider> DistanceObjects;
        private List<ILight> Lights;

        private Vector2 WindowSize;
        private Vector2 RayBoundaryLength;

        private Camera Camera;
        private PhongManager PhongManager;

        public Renderer(Vector2 _windowSize, Vector2 _distanceBoundary)
        {
            DistanceObjects = new List<IDistanceProvider>();

            WindowSize = _windowSize;
            RayBoundaryLength = _distanceBoundary;

            InitializeCamera();
            InitializeManagers();
        }

        #region Initializers
        private void InitializeCamera()
        {
            Camera = new Camera(new Vector2(1, 6f / 8), WindowSize, 1f, 0);
            Camera.PointAt(new Vector3(5, 5, 5));
            Camera.Place(new Vector3(0, 0, 0));

            Camera.Update();
        }

        private void InitializeManagers()
        {
            PhongManager = new PhongManager(Color.White);
        }
        #endregion

        public Color[,] Render()
        {
            Color[,] Pixels = new Color[(int)WindowSize.X, (int)WindowSize.Y];
            Vector3 LocalRayDirection;

            Trace.WriteLine(DateTime.Now);

            for (int i = 0; i < WindowSize.X; i++)
            {
                for(int j = 0; j < WindowSize.Y; j++)
                {
                    LocalRayDirection = Camera.GetRayDirection(i, j);
                    Pixels[i,j] = ComputeRay(Camera.Position, LocalRayDirection);
                }
            }
            Trace.WriteLine(DateTime.Now);

            return Pixels;
        }

        public void SetScene(List<IDistanceProvider> _distanceObjects, List<ILight> _lights)
        {
            DistanceObjects = _distanceObjects;
            Lights = _lights;
        }

        private Color ComputeRay(Vector3 RayStartingPos, Vector3 RayDirection)
        {
            float FailSafeCount = 1000;
            int Iteriations = 0;
            Vector3 RayPosition = RayStartingPos;

            (float Distance, IDistanceProvider MinDistObject) = GetMinimumDistance(RayPosition);

            while (Distance > RayBoundaryLength.X && Distance < RayBoundaryLength.Y)
            {
                FailSafeCount--;
                if(FailSafeCount < 0)
                {
                    break;
                }

                Iteriations++;

                (Distance, MinDistObject) = GetMinimumDistance(RayPosition);
                RayPosition += RayDirection * Distance;
            }

            if(Distance < RayBoundaryLength.X)
            {
                return ComputeColor(MinDistObject.LightingCoefs);
            }
            else
            {
                return Color.Black;
            }
        }

        private Color ComputeColor(Vector3 LightingCoefs)
        {
            return PhongManager.GetColor(LightingCoefs, Camera.Position, Lights);
        }

        private Tuple<float, IDistanceProvider> GetMinimumDistance(Vector3 Position)
        {
            float MinDistance = float.PositiveInfinity;

            IDistanceProvider MinDistanceProvider = null;

            float CurrentDistance;
            foreach(var DistanceObject in DistanceObjects)
            {
                CurrentDistance = DistanceObject.GetDistance(Position);
                if (CurrentDistance < MinDistance)
                {
                    MinDistance = CurrentDistance;
                    MinDistanceProvider = DistanceObject;
                }
            }

            return new Tuple<float, IDistanceProvider>(MinDistance, MinDistanceProvider);
        }
    }
}
