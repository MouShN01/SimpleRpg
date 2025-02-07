using UnityEngine;
using R3;

namespace Camera
{
    public class CameraView : MonoBehaviour
    {
        public Transform player; // Персонаж
        public Vector3 offset = new Vector3(0, 5, -5); // Смещение камеры
        public float rotationSpeed = 2f; // Скорость вращения

        private CameraViewModel viewModel;
        private Vector3 currentOffset;

        void Start()
        {
            viewModel = new CameraViewModel();
            currentOffset = offset;

            // Подписка на обновление позиции камеры
            viewModel.CameraPosition
                .Subscribe(pos => transform.position = pos)
                .AddTo(this);

            viewModel.CameraRotation
                .Subscribe(rot => transform.rotation = rot)
                .AddTo(this);
        }
        
        void LateUpdate() {
            if (player == null) return;

            // Управляем вращением камеры мышью
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion camTurnAngle = Quaternion.Euler(vertical, horizontal, 0);
            currentOffset = camTurnAngle * currentOffset;

            // Обновляем позицию и поворот камеры
            Vector3 targetPosition = player.position + currentOffset;
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);

            viewModel.UpdateCamera(targetPosition, targetRotation);
        }

        public CameraViewModel GetViewModel() {
            return viewModel;
        }
    }
}