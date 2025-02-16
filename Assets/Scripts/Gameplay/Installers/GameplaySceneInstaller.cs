using Camera;
using Other.Chest;
using Other.Items;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private ItemView goldPrefab;
    [SerializeField] private ItemView armorPrefab;
    [SerializeField] private UnityEngine.Camera cam;
    [SerializeField] private Transform[] goldSpawnPoints;
    [SerializeField] private Transform[] armorSpawnPoints;

    public override void InstallBindings()
    {
        Container.Bind<UnityEngine.Camera>().FromInstance(cam).AsSingle();
        
        Container.Bind<ChestModel>().AsSingle();
        Container.Bind<ChestViewModel>().AsSingle();
        
        var chest = Container.InstantiatePrefabForComponent<ChestView>(chestPrefab);
        Container.Bind<ChestView>().FromInstance(chest).AsSingle();
        
        SpawnGold();
        SpawnArmor();
    }

    public void SpawnGold()
    {
        foreach (var spawnPoint in goldSpawnPoints)
        {
            var gold = Container.InstantiatePrefabForComponent<ItemView>(goldPrefab, spawnPoint.position, Quaternion.identity, null);
            gold.Initialize(new GoldModel(50));
        }
    }

    public void SpawnArmor()
    {
        foreach (var spawnPoint in armorSpawnPoints)
        {
            var armor = Container.InstantiatePrefabForComponent<ItemView>(armorPrefab, spawnPoint.position, Quaternion.Euler(-90, 0, 0), null);
            armor.Initialize(new ArmorModel());
        }
        
    }
}
