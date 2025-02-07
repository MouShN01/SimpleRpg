using System;
using Hero;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraFollower:MonoBehaviour
    {
        private Transform _heroTransform;
        public Vector3 offset = new Vector3(0, 5, -5); // Смещение камеры
        public float rotationSpeed = 2f; // Скорость вращения

        public Vector3 currentOffset;

        [Inject]
        public void Instantiate(HeroView heroView)
        {
            _heroTransform = heroView.transform;
        }

        private void Start()
        {
            currentOffset = offset;
        }

        private void LateUpdate()
        {
            // Управляем вращением камеры мышью
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion camTurnAngle = Quaternion.Euler(vertical, horizontal, 0);
            currentOffset = camTurnAngle * currentOffset;

            // Обновляем позицию и поворот камеры
            Vector3 targetPosition = _heroTransform.position + currentOffset;
            Quaternion targetRotation = Quaternion.LookRotation(_heroTransform.position - transform.position);

            transform.position = targetPosition;
            transform.LookAt(_heroTransform.position + Vector3.up * 1.5f);
        }
    }
}