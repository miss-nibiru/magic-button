using UnityEngine;

/// <summary>
/// Controls game start, restart and over when character dies. Also controls pauses between waves
/// Shows UI when the waves change
/// </summary>

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private IGameState _currentState;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        _currentState?.ExecuteState();
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.StopState();
        _currentState = newState;

        _currentState?.StartState();
    }
    
    public void BishIsDead()
    {
        GameOverState gameOverState = new GameOverState(gameOverCanvas);
        ChangeState(gameOverState);
    }
    
}
