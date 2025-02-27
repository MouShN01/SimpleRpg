using System;
using System.Collections.Generic;
using System.Linq;
using Other;
using Other.Items;
using R3;
using UnityEngine;

namespace Hero
{
    public class HeroViewModel : IDisposable
    {
        public ReactiveProperty<float> Health  = new();
        public ReactiveProperty<float> Stamina = new();
        public ReactiveProperty<Vector3> Position = new();
        public ReactiveProperty<Quaternion> Rotation = new(); 
        public ReactiveProperty<bool> IsGrounded = new(true);
        public ReactiveProperty<bool> IsSprinting = new(false);
        public ReactiveProperty<IEnumerable<IItem>> Inventory { get; } = new();
        
        private readonly CompositeDisposable disposables = new();
        
        private readonly HeroModel _model;
        private readonly Transform _cameraTransform;
        
        private Vector3 _velocity; // Скорость персонажа по Y
        private float _gravity = -9.8f;
        private float _groundCheckDistance = 1.0f; // Дистанция проверки земли
        private LayerMask _groundLayer = LayerMask.GetMask("Ground");

        private float normalSpeed = 5.0f;
        private float sprintSpeed = 10.0f;
        
        private float staminaCost = 0.5f;
        private float staminaRegen = 0.6f;

        public HeroViewModel(HeroModel model, Transform cameraTransform)
        {
            _model = model;
            _cameraTransform = cameraTransform;
            
            IsSprinting
                .Subscribe(isSprinting=>_model.Speed.Value = isSprinting? sprintSpeed : normalSpeed)
                .AddTo(disposables);
            
            _model.Health
                .Subscribe(health => Health.Value = Mathf.Clamp(health, 0, 100))
                .AddTo(disposables);

            _model.Stamina
                .Subscribe(stamina => 
                {
                    Stamina.Value = Mathf.Clamp(stamina, 0, 100);
                    if (Stamina.Value <= 0)
                    {
                        IsSprinting.Value = false; // Отключаем спринт при 0 выносливости
                    }
                })
                .AddTo(disposables);
            
            _model.Inventory
                .Subscribe(items => Inventory.Value = items)
                .AddTo(disposables);

        }

        public void Move(Vector2 input)
        {
            if (input.magnitude == 0 && IsGrounded.Value) return;

            // Получаем направление камеры (без учёта высоты)
            Vector3 cameraForward = _cameraTransform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = _cameraTransform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            // Рассчитываем направление движения
            Vector3 moveDirection = cameraForward * input.y + cameraRight * input.x;
            moveDirection.Normalize();
            
            // Проверяем, стоит ли персонаж на земле
            IsGrounded.Value = Physics.Raycast(Position.Value, Vector3.down, out RaycastHit hit, _groundCheckDistance, _groundLayer);
            
            if (IsGrounded.Value && _velocity.y < 0)
            {
                Position.Value = new Vector3(Position.Value.x, hit.point.y + 1.0f, Position.Value.z);
                _velocity.y = 0;
            }
            else
            {
                _velocity.y += _gravity * Time.fixedDeltaTime; // Применяем гравитацию
            }
            
            // Обновляем позицию
            Position.Value += moveDirection * _model.Speed.Value * Time.deltaTime;
            Position.Value += _velocity * Time.deltaTime;

            // Обновляем поворот персонажа
            if (moveDirection != Vector3.zero)
            {
                Rotation.Value = Quaternion.LookRotation(moveDirection);
            }
        }
        
        public void Jump()
        {
            if (IsGrounded.Value)
            {
                _velocity.y = _model.JumpForce.Value; // Применяем силу прыжка
                IsGrounded.Value = false;
            }
        }

        public void TakeDamage(float damage)
        {
            _model.Health.Value = Mathf.Clamp(_model.Health.Value - damage, 0, 100);
            Debug.Log(_model.Health.Value);
        }

        public void Run()
        {
            if (Stamina.Value > 0)
            {
                IsSprinting.Value = true;
                UseStamina();
            }
        }

        public void Heal(float heal)
        {
            _model.Health.Value = Mathf.Clamp(_model.Health.Value + heal, 0, 100);
            Debug.Log(_model.Health.Value);
        }

        public void UseStamina()
        {
            _model.Stamina.Value = Mathf.Clamp(_model.Stamina.Value - staminaCost, 0, 100);
            Debug.Log(_model.Stamina.Value);
        }

        public void RegenerateStamina()
        {
            if (Stamina.Value < 100)
            {
                _model.Stamina.Value = Mathf.Clamp(_model.Stamina.Value + staminaRegen, 0, 100);
                Debug.Log(_model.Stamina.Value);
            }
        }

        public void GetItem(IItem item)
        {
            if (_model.Inventory.Value == null)
            {
                _model.Inventory.Value = new List<IItem>(); // Безопасная инициализация
            }

            var updatedInventory = _model.Inventory.Value.ToList();

            var existingItem = updatedInventory.FirstOrDefault(i => i.Name == item.Name && i.IsStackable);

            if (existingItem != null && existingItem is ItemModel baseItem)
            {
                baseItem.AddQuantity(item.Quantity);
                Debug.Log($"Updated: {existingItem.Name} x{existingItem.Quantity}");
            }
            else
            {
                updatedInventory.Add(item);
                Debug.Log($"Added: {item.Name} x{item.Quantity}");
            }

            _model.Inventory.Value = updatedInventory;
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}