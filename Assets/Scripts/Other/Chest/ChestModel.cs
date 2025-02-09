using System.Collections.Generic;
using System.Numerics;
using R3;

namespace Other.Chest
{
    public class ChestModel
    {
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        
        public List<IItem> Items = new();
    }
}