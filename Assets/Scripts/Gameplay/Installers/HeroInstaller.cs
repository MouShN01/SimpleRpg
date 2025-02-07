using Hero;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class HeroInstaller:MonoInstaller
    {
        [SerializeField] private HeroView heroPrefab;
        [SerializeField] private Transform cameraTransform;
        public override void InstallBindings()
        {
            Container.Bind<HeroModel>().AsSingle();
            Container.Bind<HeroViewModel>().AsSingle().WithArguments(cameraTransform);

            var hero = Container.InstantiatePrefabForComponent<HeroView>(heroPrefab);
            Container.Bind<HeroView>().FromInstance(hero).AsSingle();
        }
    }
}