using System.Numerics;

namespace raymarching.ComputationClasses
{
    class Camera
    {
        private Vector3 CameraPosition { get; set; }
        private Vector3 CameraTarget { get; set; }

        private Vector2 SensorSize;
        private Vector2 Resolution;
        private float FocalLength;
        private float ViewTensorsAngle;

        private Vector3 CameraVector;
        private Vector3 SensorYTensor, SensorXTensor;
        private readonly Vector3 XTensor = new Vector3(0, 0, 1);

        Camera(Vector2 _sensorSize, Vector2 _resolution, float _focalLength, float _viewTensorAngle)
        {
            CameraPosition = new Vector3();
            CameraTarget = new Vector3(1, 1, 1);

            SensorSize = _sensorSize;
            Resolution = _resolution;
            FocalLength = _focalLength;
            ViewTensorsAngle = _viewTensorAngle;
        }

        private void UpdateLocalCoords()
        {
            SensorXTensor = Vector3.Normalize(Vector3.Cross(XTensor, CameraVector));
            SensorYTensor = Vector3.Normalize(Vector3.Cross(SensorXTensor, CameraVector));
        }

        private void UpdateCameraVector()
        {
            CameraVector = Vector3.Normalize(CameraTarget - CameraPosition) * FocalLength;
        }

        private void RotateViewTensors(float angle)
        {
            Quaternion RotationMatrix = new Quaternion(CameraVector, angle);

            Vector3.Transform(SensorYTensor, RotationMatrix);
            Vector3.Transform(SensorXTensor, RotationMatrix);
        }

        public void UpdateCamera()
        {
            UpdateLocalCoords();
            RotateViewTensors(ViewTensorsAngle);
            UpdateCameraVector();
        }

        public Vector3 GetRayDirection(int PosX, int PosY)
        {
            Vector3 SensorGlobalX = SensorSize.X * SensorXTensor * (PosX / Resolution.X - 0.5f);
            Vector3 SensorGlobalY = SensorSize.Y * SensorYTensor * (PosY / Resolution.Y - 0.5f);

            return CameraVector + SensorGlobalX + SensorGlobalY;
        }
    }
}
