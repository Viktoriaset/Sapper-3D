using UnityEngine;
using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    [SerializeField] private GameController gameControllerPrefab;
    public override void InstallBindings()
    {
        print("I am here");
        var gameController = 
            Container.InstantiatePrefabForComponent<GameController>(gameControllerPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<GameController>().FromInstance(gameController).AsSingle().NonLazy();
    }
}