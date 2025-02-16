using Hero;
using UnityEngine;

namespace Other.Items
{
    public class ItemViewModel
    {
        
        private IItem _item;
        
        public string Name => _item.Name;
        public int Quantity => _item.Quantity;

        public ItemViewModel(IItem item)
        {
            _item = item;
        }

        public void PickUp(HeroView hero)
        {
            hero.TakeItem(_item);
            Debug.Log($"Picked up: {_item.Name} x{_item.Quantity}, Stackable: {_item.IsStackable}");
        }
    }
}