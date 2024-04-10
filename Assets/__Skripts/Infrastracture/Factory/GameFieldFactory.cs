using UnityEngine;
using Zenject;

public class GameFieldFactory: IGameFieldFactory
{
    private const string gameFeild = "GameField";
    private DiContainer _diConteiner;
    private Object _gameFieldPrefab;

    public GameFieldFactory(DiContainer diContainer)
    {
        _diConteiner = diContainer;
    }

    public void Load()
    {
        _gameFieldPrefab = Resources.Load(gameFeild);
    }

    public void Create(Vector3 at)
    {
        _diConteiner.InstantiatePrefab(_gameFieldPrefab, at, Quaternion.identity, null);
    }
}
