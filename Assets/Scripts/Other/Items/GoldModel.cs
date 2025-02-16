using UnityEngine;

namespace Other.Items
{
    public class GoldModel : ItemModel
    {
        public GoldModel(int amount)
        {
            Name = "Gold";
            IsStackable = true;
            Quantity = amount;
            Icon = Resources.Load<Sprite>("Items/Coin");
        }
    }
}