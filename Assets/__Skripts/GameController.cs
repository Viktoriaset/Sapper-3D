using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private enum eGameState
    {
        playing,

        gameOver,

    }

    [Header("Set in inspector")]
    [SerializeField] private const string mainMenuScene = "MainMenu";
    [SerializeField] private int gameOverDurationInMilliseconds = 200;
    [HideInInspector] public List<Cell> BombCells;

    private eGameState gameState = eGameState.playing;

    public async void GameOver()
    {
        if (gameState != eGameState.playing)
            return;

        gameState = eGameState.gameOver;

        foreach(Cell cell in BombCells)
            cell.Open();

        await UniTask.Delay(gameOverDurationInMilliseconds);

        Exit();
    }

    public void Exit()
    {
        Cell.bombCount = 0;
        Face.ClearEmployedBoundaryPosition();
        SceneManager.LoadSceneAsync(mainMenuScene);
    }
}