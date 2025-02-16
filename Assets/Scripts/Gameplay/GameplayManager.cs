using Hero;
using Other.Chest;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        private HeroViewModel _heroViewModel;
        private ChestViewModel _chestViewModel;
        
        [Inject]
        public void Construct(HeroViewModel heroViewModel, ChestViewModel chestViewModel)
        {
            _heroViewModel = heroViewModel;
            _chestViewModel = chestViewModel;
        }
    }
}