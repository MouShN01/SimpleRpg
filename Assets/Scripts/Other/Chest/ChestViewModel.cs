using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Hero;
using R3;

namespace Other.Chest
{
    public class ChestViewModel
    {
        private readonly ChestModel _model;
        public ReactiveProperty<IEnumerable<IItem>> Items { get;} = new();

        public ChestViewModel(ChestModel model)
        {
            _model = model;
            Items = new ReactiveProperty<IEnumerable<IItem>>(_model.Items);
        }

        public void TakeItem(IItem item, HeroViewModel heroViewModel)
        {
            if(_model.Items.Contains(item))
            {
                _model.Items.Remove(item);
                Items.Value = _model.Items.ToList();

                
            }
        }
    }
}