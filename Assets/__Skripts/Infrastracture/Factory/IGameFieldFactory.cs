using UnityEngine;

public interface IGameFieldFactory
{
    void Load();
    void Create(Vector3 at);
}
