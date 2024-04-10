
using UnityEngine;

public interface ICellFactory
{
    void Load();
    GameObject Create(Vector3 at, Transform parent);
}