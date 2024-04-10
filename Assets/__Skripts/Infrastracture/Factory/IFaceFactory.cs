using UnityEngine;

public interface IFaceFactory
{
    void Load();
    GameObject Create(Transform transform, Transform parent);
}
