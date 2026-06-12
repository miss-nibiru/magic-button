using UnityEngine;

/// <summary>
/// Controls the waves in the game
/// </summary>

public interface IGameState
{

    void StartState();
    void ExecuteState();
    void StopState();

}
