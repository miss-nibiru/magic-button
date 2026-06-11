using UnityEngine;

/// <summary>
/// This state controls Wave One.
/// It decides what monster spawns and how many spawn
/// </summary>

public class WaveEndState : IWaveState
{
    private WaveStateMachine _waveStateMachine;
    private MonsterSpawner _monsterSpawner;
    private MonsterManager _monsterManager;
    private GameObject[] _monsterPrefab;

    private int _totalMonstersToSpawn;
    private int _maxActiveMonsters;
    private int _currentSpawnCount;

    private bool _waveComplete;

    public WaveEndState(
        WaveStateMachine waveStateMachine,
        MonsterSpawner monsterSpawner,
        MonsterManager monsterManager,
        GameObject[] monsterPrefab,
        int totalMonstersToSpawn,
        int maxActiveMonsters) // this can't be okay, there should be a better way to do this
    {
        _waveStateMachine = waveStateMachine;
        _monsterSpawner = monsterSpawner;
        _monsterManager = monsterManager;
        _monsterPrefab = monsterPrefab;
        _totalMonstersToSpawn = totalMonstersToSpawn;
        _maxActiveMonsters = maxActiveMonsters;
    }

    public void StartWave()
    {
        Debug.Log("THE END!");

        _currentSpawnCount = 0;
        _waveComplete = false;
    }

    public void ExecuteWave()
    {
        if (_waveComplete) return;

        bool stillNeedsToSpawn = _currentSpawnCount < _totalMonstersToSpawn;
        bool roomOnScreen = _monsterManager.ActiveMonsterCount < _maxActiveMonsters;

        if (stillNeedsToSpawn && roomOnScreen && _monsterSpawner.CanSpawn)
        {
            _monsterSpawner.SpawnMonster(_monsterPrefab);
            _currentSpawnCount++;
        }

        bool finishedSpawning = _currentSpawnCount >= _totalMonstersToSpawn;
        bool noMonstersLeft = _monsterManager.ActiveMonsterCount == 0;

        if (finishedSpawning && noMonstersLeft)
        {
            _waveComplete = true;
            Debug.Log("THE END");
            
            //REMINDER TO CREATE A GAME WON STATE THAT SHOWS THE PLAYER THE GAME ENDED AND ASK IF CONTINUE OR NOT
            
        }
    }

    public void StopWave()
    {
        Debug.Log("THE END");
    }
}