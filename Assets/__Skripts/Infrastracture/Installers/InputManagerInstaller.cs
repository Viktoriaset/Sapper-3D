using UnityEngine;
using Zenject;

public class InputManagerInstaller : MonoInstaller
{
    [SerializeField] private InputManagerPC inputManagerPC;
    [SerializeField] private InputManagerMobile inputManagerMobile;
    public override void InstallBindings()
    {
        InputManager inputManager;
        if (WebGLCheckBrowser.IsMobileBrowser())
        {
            inputManager = 
            Container.InstantiatePrefabForComponent<InputManagerMobile>(inputManagerMobile, Vector3.zero, Quaternion.identity, null);
        }
        else 
        {
            inputManager = 
            Container.InstantiatePrefabForComponent<InputManagerPC>(inputManagerPC, Vector3.zero, Quaternion.identity, null);
        }

        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
    }
}