using UnityEngine;

namespace Other.Items
{
    public class ArmorModel : ItemModel
    {
        public ArmorModel()
        {
            Name = "Armor";
            IsStackable = false;
            Quantity = 1;
            Icon = Resources.Load<Sprite>("Items/Armor");
        }
    }
}