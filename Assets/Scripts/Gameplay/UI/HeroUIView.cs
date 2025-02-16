using System;
using System.Collections.Generic;
using Hero;
using Other;
using Other.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI
{
    public class HeroUIView:MonoBehaviour
    {
        [SerializeField]private Slider healthSlider;
        [SerializeField]private TMP_Text healthText;
        
        [SerializeField]private Slider staminaSlider;
        [SerializeField]private TMP_Text staminaText;
        
        [SerializeField]private GameObject inventoryPanel;
        [SerializeField]private Transform itemContainer;
        [SerializeField]private GameObject itemPrefab;
        
        private HeroViewModel _heroViewModel;
        private List<GameObject> _itemSlots = new();

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
            
            _heroViewModel.Inventory
                .Subscribe(UpdateInventory)
                .AddTo(this);
            
            inventoryPanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }

        private void ToggleInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
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
            int staminaToDisplay = (int)value;
            staminaText.text = staminaToDisplay.ToString();
        }

        private void UpdateInventory(IEnumerable<IItem> items)
        {
            foreach (var slot in _itemSlots)
            {
                Destroy(slot);
            }
            _itemSlots.Clear();

            foreach (var item in items)
            {
                var slotObj = Instantiate(itemPrefab, itemContainer);
                var slot = slotObj.GetComponent<SlotView>();
                slot.SetItem(item);
                _itemSlots.Add(slotObj);
            }
        }
    }
}