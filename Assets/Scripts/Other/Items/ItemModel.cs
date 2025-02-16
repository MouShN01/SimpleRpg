using UnityEngine;

namespace Other.Items
{
    public abstract class ItemModel : IItem
    {
        public string Name { get; protected set; }
        public bool IsStackable { get; protected set; }
        public int Quantity { get; protected set; }
        
        public Sprite Icon { get; protected set; }
        
        // Новый метод для изменения количества
        public void AddQuantity(int amount)
        {
            Quantity += amount;
        }
    }
}