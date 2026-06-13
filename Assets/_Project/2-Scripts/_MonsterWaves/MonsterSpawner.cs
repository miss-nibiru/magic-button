using UnityEngine;

/// <summary>
/// Knows how to spawn a monster on the grid.
/// Each monster knows what column to spawn in, the spawner just creates it.
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private MainGrid mainGrid;
    [SerializeField] private PlayerHealth playerHealth;
    
    [SerializeField] private float firstSpawnTime = 1f;
    private float _spawnTimer;
    
    public bool CanSpawn => _spawnTimer <= 0f;

    private void Start()
    {
        _spawnTimer = firstSpawnTime;
    }

    private void Update()
    {
        if (_spawnTimer > 0f)
        {
            _spawnTimer -= Time.deltaTime;
        }
    }
    
    public void SpawnMonster(GameObject monsterPrefab)
    {
        if (!monsterPrefab) return;

        MonsterController prefabMonster = monsterPrefab.GetComponent<MonsterController>();

        if (!prefabMonster) return;

        MonsterData monsterData = prefabMonster.MonsterData;

        if (!monsterData) return;

        int spawnColumn = monsterData.StartingColumn;

        if (monsterManager.MonsterInColumn(spawnColumn))
        {
            Debug.Log("Blocked monster spawn. Column already occupied: " + spawnColumn);

            _spawnTimer = firstSpawnTime;
            return;
        }

        GameObject spawnedMonster = Instantiate(monsterPrefab);

        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();

        if (!monsterController) return;

        monsterController.InitializeMonster(mainGrid, playerHealth);
        monsterManager.DetectBicho(monsterController);

        _spawnTimer = firstSpawnTime;
    }
    
    public void SpawnMonster(GameObject[] monsterPrefabs)
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;

        GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

        SpawnMonster(randomPrefab);
    }
    
}