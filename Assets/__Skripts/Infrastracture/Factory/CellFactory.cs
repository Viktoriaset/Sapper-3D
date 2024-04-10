using UnityEngine;
using Zenject;

public class CellFactory : ICellFactory
{
    private const string cell = "CubeCell";
    private readonly DiContainer _diContainer;
    private readonly GameController _gameController;
    private Object _cellPrefab;

    public CellFactory(DiContainer diContainer, GameController gameController)
    {
        _diContainer = diContainer;
        _gameController = gameController;
    }

    public void Load()
    {
        _cellPrefab = Resources.Load(cell);
    }

    public GameObject Create(Vector3 at, Transform parent)
    {
        GameObject cellGo = _diContainer.InstantiatePrefab(_cellPrefab, at, Quaternion.identity, parent);
        cellGo.GetComponent<Cell>().Constructor(_gameController);

        return cellGo;
    }
}