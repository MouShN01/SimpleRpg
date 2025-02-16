using System;
using Camera;
using Other;
using R3;
using UnityEngine;
using Zenject;

namespace Hero
{
    public class HeroView : MonoBehaviour
    {
        private HeroViewModel _viewModel;

        [Inject]
        public void Initialize(HeroViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _viewModel.Position.Subscribe(pos => transform.position = pos).AddTo(this);
            _viewModel.Rotation.Subscribe(rot => transform.rotation = rot).AddTo(this);
            
        }
        
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _viewModel.Jump();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _viewModel.Run();
            }
            else
            {
                _viewModel.IsSprinting.Value = false;
                _viewModel.RegenerateStamina();
            }
            
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _viewModel.Move(direction);
        }

        public void TakeDamage(float damage)
        {
            _viewModel.TakeDamage(damage);
        }

        public void Heal(float heal)
        {
            _viewModel.Heal(heal);
        }

        public void TakeItem(IItem item)
        {
            _viewModel.GetItem(item);
        }
    }
}