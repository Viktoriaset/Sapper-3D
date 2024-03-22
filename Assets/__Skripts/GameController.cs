using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Properties
    [Header("Set in inspector")]
    [SerializeField] private float gameOverDuration = 2f;
    [HideInInspector] public List<Cell> BombCells;
    #endregion

    #region Methods
    public void GameOver()
    {
        foreach(Cell cell in BombCells)
            cell.Open();

        Invoke("Exit", gameOverDuration);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region MonoBehavior Methods
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    #endregion
}
