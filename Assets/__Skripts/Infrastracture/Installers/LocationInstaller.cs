using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private GameObject gameControllerPrefab;
    public override void InstallBindings()
    {
        BindGameController();
        BindCellFactory();
        BindFaceFactory();
        BindGameFieldFactory();
    }

    private void BindGameController()
    {
        GameController gameController =
                    Container
                    .InstantiatePrefabForComponent<GameController>(gameControllerPrefab);
        Container.Bind<GameController>().FromInstance(gameController).AsSingle().NonLazy();
    }

    private void BindGameFieldFactory()
    {
        Container
            .Bind<IGameFieldFactory>()
            .To<GameFieldFactory>()
            .AsSingle()
            .NonLazy();
    }


    private void BindFaceFactory()
    {
        Container
        .Bind<IFaceFactory>()
        .To<FaceFactory>()
        .AsSingle()
        .NonLazy();
    }

    private void BindCellFactory()
    {
        Container
        .Bind<ICellFactory>()
        .To<CellFactory>()
        .AsSingle()
        .NonLazy();
    }

}