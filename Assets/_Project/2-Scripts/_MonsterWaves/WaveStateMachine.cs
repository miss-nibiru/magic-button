using UnityEngine;
public class WaveStateMachine : MonoBehaviour
{
    
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private WaveWeaknessUI weaknessUI;
    [SerializeField] private SpellBookUI spellBookUI;
    
    //THIS IS ALL FOR WAVE ONE ONLY
    [SerializeField] private GameObject firstWaveMonsterPrefab;
    [SerializeField] private int firstWaveTotal;
    [SerializeField] private int firstWaveMax;
    
    //SECOND WAVE
    [SerializeField] private GameObject[] secondWavePrefab;
    [SerializeField] private int secondWaveTotal;
    [SerializeField] private int secondWaveMax;
    
    //THIRD WAVE
    [SerializeField] private GameObject[] thirdWavePrefab;
    [SerializeField] private int thirdWaveTotal;
    [SerializeField] private int thirdWaveMax;
    
    //FOURTH WAVE
    [SerializeField] private GameObject[] fourthWavePrefab;
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
            firstWaveMonsterPrefab,
            firstWaveTotal,
            firstWaveMax
        );

        weaknessUI.ShowWaveWeaknessBanners(firstWaveMonsterPrefab);
        UpdateBookSpells(firstWaveMonsterPrefab);
        ChangeState(waveOneState);
    }

    public void StartSecondWave()
    {

        WaveTwoState waveTwoState = new WaveTwoState(
            this,
            monsterSpawner,
            monsterManager,
            secondWavePrefab,
            secondWaveTotal,
            secondWaveMax
        );

        weaknessUI.ShowWaveWeaknessBanners(secondWavePrefab);
        UpdateBookSpells(secondWavePrefab);
        ChangeState(waveTwoState);

    }

    public void StartThirdWave()
    {
        WaveThreeState waveThreeState = new WaveThreeState(
            this,
            monsterSpawner,
            monsterManager,
            thirdWavePrefab,
            thirdWaveTotal,
            thirdWaveMax
            );
        
        weaknessUI.ShowWaveWeaknessBanners(thirdWavePrefab);
        UpdateBookSpells(thirdWavePrefab);
        ChangeState(waveThreeState);
        
    }
    
    public void StartFourthWave()
    {
        WaveFourState waveFourState = new WaveFourState(
            this,
            monsterSpawner,
            monsterManager,
            fourthWavePrefab,
            fourthWaveTotal,
            fourthWaveMax
        );
        
        weaknessUI.ShowWaveWeaknessBanners(fourthWavePrefab);
        UpdateBookSpells(fourthWavePrefab);
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
        
        weaknessUI.ShowWaveWeaknessBanners(finalPrefabs);
        UpdateBookSpells(finalPrefabs);
        ChangeState(waveEndState);
    }

    
    public void ChangeState(IWaveState newState)
    {
        _currentState?.StopWave();

        _currentState = newState;

        _currentState?.StartWave();
    }

    private void UpdateBookSpells(GameObject[] monsterPrefabs)
    {
        SpellData[] neededSpells = new SpellData[monsterPrefabs.Length];
        for (int i = 0; i < monsterPrefabs.Length; i++)

        {
            
            MonsterController monsterController  = monsterPrefabs[i].GetComponent<MonsterController>();
            
            if (!monsterController) continue;

            neededSpells[i] = monsterController.MonsterData.MonsterWeakness;

        }
        
        spellBookUI.ShowUsefulSpells(neededSpells);
        
    }
    
    private void UpdateBookSpells(GameObject monsterPrefab)
    {
        UpdateBookSpells(new GameObject[] { monsterPrefab });
    }
    
    
}

