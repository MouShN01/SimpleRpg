using System.Collections.Generic;
using System.Numerics;
using R3;

namespace Other.Chest
{
    public class ChestViewModel
    {
        private readonly ChestModel _model;
        public List<IItem> Items { get; set; }

        public ChestViewModel(ChestModel model)
        {
            _model = model;
            Items = new List<IItem>(_model.Items);
        }
    }
}