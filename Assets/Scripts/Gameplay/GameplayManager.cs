using Hero;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        private HeroViewModel _heroViewModel;

        [Inject]
        public void Construct(HeroViewModel heroViewModel)
        {
            _heroViewModel = heroViewModel;
        }
    }
}