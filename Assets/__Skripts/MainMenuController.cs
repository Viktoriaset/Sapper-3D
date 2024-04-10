using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string coreGameScene = "CoreGameScene";
    public void Play()
    {
        SceneManager.LoadScene(coreGameScene);
    }
}
