using UnityEngine;
using Zenject;

public class FaceFactory : IFaceFactory
{
    private const string face = "Face";
    private readonly DiContainer _diContainer;
    private readonly ICellFactory _cellFactory;
    private Object _facePrefab;

    public FaceFactory(DiContainer diContainer, ICellFactory cellFactory)
    {
        _diContainer = diContainer;
        _cellFactory = cellFactory;
    }

    public void Load()
    {
        _facePrefab = Resources.Load(face);
        _cellFactory.Load();
    }

    public GameObject Create(Transform transform, Transform parent)
    {
        GameObject faceGo =
            _diContainer.InstantiatePrefab(_facePrefab, transform.position, transform.rotation, parent);
        faceGo.GetComponent<Face>().Constructor(_cellFactory);

        return faceGo;
    }
}
