using Other.Chest;
using UnityEngine;
using Zenject;

namespace Other.UI
{
    public class ChestUIView : MonoBehaviour
    {
        
        private ChestViewModel _viewModel;

        [Inject]
        public void Construct(ChestViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}