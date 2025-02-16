using UnityEngine;

namespace Other
{
    public interface IItem
    {
        string Name { get; }
        bool IsStackable { get; }
        int Quantity { get; }
        Sprite Icon { get; }
    }
}