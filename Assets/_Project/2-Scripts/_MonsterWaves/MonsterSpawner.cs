using UnityEngine;
using System.Collections.Generic;

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

        int spawnColumn = ChooseSpawnColumn(monsterData);

        if (spawnColumn < 0)
        {
            _spawnTimer = firstSpawnTime;
            return;
        }

        GameObject spawnedMonster = Instantiate(monsterPrefab);

        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();

        if (!monsterController) return;

        monsterController.InitializeMonster(mainGrid, playerHealth, spawnColumn);
        monsterManager.DetectBicho(monsterController);

        _spawnTimer = firstSpawnTime;
    }
    
    private int ChooseSpawnColumn(MonsterData monsterData)
    {
        List<int> validColumns = new List<int>();

        for (int column = 0; column < mainGrid.gridSize; column++)
        {
            if (monsterManager.CanSpawnAtColumn(monsterData, column))
            {
                validColumns.Add(column);
            }
        }

        if (validColumns.Count == 0)
            return -1;

        if (monsterData.RandomSpacing)
        {
            int randomIndex = Random.Range(0, validColumns.Count);
            return validColumns[randomIndex];
        }

        if (validColumns.Contains(monsterData.StartingColumn))
        {
            return monsterData.StartingColumn;
        }

        return validColumns[validColumns.Count - 1];
    }
    
    public void SpawnMonster(GameObject[] monsterPrefabs)
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;

        GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

        SpawnMonster(randomPrefab);
    }
    
}