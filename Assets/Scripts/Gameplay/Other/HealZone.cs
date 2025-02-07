using Hero;
using UnityEngine;

namespace Gameplay.Other
{
    public class HealZone : MonoBehaviour
    {
        private float _heal = 0.1f;
        private HeroView _hero;
        private bool _isInZone = false;

        private void Update()
        {
            if (_isInZone)
            {
                Damage(_hero);
            }
        }

        private void Damage(HeroView hero)
        {
            if (hero != null)
            {
                hero.Heal(_heal);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _hero = other.GetComponent<HeroView>();
            _isInZone = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _isInZone = false;
        }
    }
}