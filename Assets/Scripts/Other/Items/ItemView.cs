using Hero;
using Unity.VisualScripting;
using UnityEngine;

namespace Other.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemInfoLabel toolTip;
        private HeroView _hero;
        private ItemViewModel _itemViewModel;
        private bool _isPlayerNearby = false;
        
        public void Initialize(IItem item)
        {
            _itemViewModel = new ItemViewModel(item);
            toolTip.SetInfo(_itemViewModel.Name, _itemViewModel.Quantity);
        }
        
        private void Update()
        {
            if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
            {
                _itemViewModel.PickUp(_hero);
                Destroy(this.gameObject);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _hero = other.GetComponent<HeroView>();
                toolTip.gameObject.SetActive(true);
                _isPlayerNearby = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                toolTip.gameObject.SetActive(false);
                _isPlayerNearby = false;
            }
        }
    }
}