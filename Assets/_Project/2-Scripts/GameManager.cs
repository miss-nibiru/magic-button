using UnityEngine;

/// <summary>
/// Controls game start, restart and over when character dies. Also controls pauses between waves
/// Shows UI when the waves change
/// </summary>

public class GameManager : MonoBehaviour
{
    private IGameState _currentState;

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
        SceneLoader.Instance.GoToGameOver();
    }
    
}
