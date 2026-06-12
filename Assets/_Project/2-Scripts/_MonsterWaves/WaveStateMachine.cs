using UnityEditor;
using UnityEngine;
public class WaveStateMachine : MonoBehaviour
{
    
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private MonsterManager monsterManager;
    
    //THIS IS ALL FOR WAVE ONE ONLY
    [SerializeField] private GameObject greenSlimePrefab;
    [SerializeField] private int firstWaveTotal;
    [SerializeField] private int firstWaveMax;
    
    //SECOND WAVE
    [SerializeField] private GameObject[] slimeKitsunePrefab;
    [SerializeField] private int secondWaveTotal;
    [SerializeField] private int secondWaveMax;
    
    //THIRD WAVE
    [SerializeField] private GameObject[] kitsuneSnekPrefab;
    [SerializeField] private int thirdWaveTotal;
    [SerializeField] private int thirdWaveMax;
    
    //FOURTH WAVE
    [SerializeField] private GameObject[] slimeAttack;
    [SerializeField] private int fourthWaveTotal;
    [SerializeField] private int fourthWaveMax;
    
    //BOSS WAVE
    [SerializeField] private GameObject[] finalPrefabs;
    [SerializeField] private int finalWaveTotal;
    [SerializeField] private int finalWaveMax;
    
    private IWaveState _currentState;

    private void Start()
    {

        StartFirstWave();

    }

    private void Update()
    {
        _currentState?.ExecuteWave();
    }

    private void StartFirstWave()
    {
        WaveOneState waveOneState = new WaveOneState(
            this,
            monsterSpawner,
            monsterManager,
            greenSlimePrefab,
            firstWaveTotal,
            firstWaveMax
        );

        ChangeState(waveOneState);
    }

    public void StartSecondWave()
    {

        WaveTwoState waveTwoState = new WaveTwoState(
            this,
            monsterSpawner,
            monsterManager,
            slimeKitsunePrefab,
            secondWaveTotal,
            secondWaveMax
        );

        ChangeState(waveTwoState);

    }

    public void StartThirdWave()
    {
        WaveThreeState waveThreeState = new WaveThreeState(
            this,
            monsterSpawner,
            monsterManager,
            kitsuneSnekPrefab,
            thirdWaveTotal,
            thirdWaveMax
            );
        
        ChangeState(waveThreeState);
    }
    
    public void StartFourthWave()
    {
        WaveFourState waveFourState = new WaveFourState(
            this,
            monsterSpawner,
            monsterManager,
            slimeAttack,
            fourthWaveTotal,
            fourthWaveMax
        );
        
        ChangeState(waveFourState);
    }

    public void StartFinalWave()
    {
        WaveEndState waveEndState = new WaveEndState(
            this,
            monsterSpawner,
            monsterManager,
            finalPrefabs,
            finalWaveTotal,
            finalWaveMax
        );
        
        ChangeState(waveEndState);
    }

    
    public void ChangeState(IWaveState newState)
    {
        _currentState?.StopWave();

        _currentState = newState;

        _currentState?.StartWave();
    }
    
}

