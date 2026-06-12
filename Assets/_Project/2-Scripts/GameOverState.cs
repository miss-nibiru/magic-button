using UnityEngine;

public class GameOverState : IGameState
{
    private GameObject _gameOverCanvas;

    public GameOverState(GameObject gameOverCanvas)
    {
        _gameOverCanvas = gameOverCanvas;
    }

    public void StartState()
    {
        Debug.Log("Game Over");

        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExecuteState()
    {
        // Nothing yet.
    }

    public void StopState()
    {
        _gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}