using System;
using UnityEngine;
public class WaveStateMachine : MonoBehaviour
{
    
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private MonsterManager monsterManager;
    
    //THIS IS ALL FOR WAVE ONE ONLY

    [SerializeField] private GameObject GreenSlimePrefab;
    [SerializeField] private int firstWaveTotal;
    [SerializeField] private int firstWaveMax;
    
    private IWaveState _currentState;

    private void Start()
    {
        WaveOneState waveOneState = new WaveOneState(
            this,
            monsterSpawner,
            monsterManager,
            GreenSlimePrefab,
            firstWaveTotal,
            firstWaveMax
        );

        ChangeState(waveOneState);
    }

    private void Update()
    {
        _currentState?.ExecuteWave();
    }

    public void ChangeState(IWaveState newState)
    {
        _currentState?.StopWave();

        _currentState = newState;

        _currentState?.StartWave();
    }
    
}

