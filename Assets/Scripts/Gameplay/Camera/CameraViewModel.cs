using R3;
using UnityEngine;

namespace Camera
{
    public class CameraViewModel
    {
        public ReactiveProperty<Vector3> CameraPosition { get; } = new ReactiveProperty<Vector3>();
        public ReactiveProperty<Quaternion> CameraRotation { get; } = new ReactiveProperty<Quaternion>();

        public void UpdateCamera(Vector3 cameraPosition, Quaternion cameraRotation)
        {
            CameraPosition.Value = cameraPosition;
            CameraRotation.Value = cameraRotation;
        }
    }
}