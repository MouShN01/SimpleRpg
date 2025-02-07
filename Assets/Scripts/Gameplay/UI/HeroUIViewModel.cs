using Hero;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI
{
    public class HeroUIViewModel:MonoBehaviour
    {
        private HeroViewModel _heroViewModel;
        [SerializeField]private Slider healthSlider;
        [SerializeField]private TMP_Text healthText;
        
        [SerializeField]private Slider staminaSlider;
        [SerializeField]private TMP_Text staminaText;

        [Inject]
        public void Initialize(HeroViewModel heroViewModel)
        {
            _heroViewModel = heroViewModel;
            
            _heroViewModel.Health
                .Subscribe(UpdateHealthBar)
                .AddTo(this);
            
            _heroViewModel.Stamina
                .Subscribe(UpdateStaminaBar)
                .AddTo(this);
        }

        private void UpdateHealthBar(float value)
        {
            healthSlider.value = value/100f;
            int healthToDisplay = (int)value;
            healthText.text = healthToDisplay.ToString();
        }

        private void UpdateStaminaBar(float value)
        {
            staminaSlider.value = value/100f;
            staminaText.text = _heroViewModel.Stamina.ToString();
        }
    }
}