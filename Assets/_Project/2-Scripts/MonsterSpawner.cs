using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knows what monsters exists and spawns them on the grid
/// Each mosnter knows what column to spawn in, the spawner just spawns them
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private MainGrid mainGrid;
    [SerializeField] private PlayerHealth playerHealth;
    
    [SerializeField] private int maxMonsters;
    private int _monsterCount;
    
    private bool _canSpawn = true;
    [SerializeField] private float firstSpawnTime = 1f;
    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = firstSpawnTime;
    }

    private void Update()
    {
        
        if (!_canSpawn) return;
        _spawnTimer -= Time.deltaTime;
        
        if (_spawnTimer <= 0f)
        {
            SpawnMonster();
        }
        
        
    }
    
    
    //pick a prefab from the pool, instantiate it, 
    private void SpawnMonster()
    {
       // if (//the game state is wave 1, then the player gets only green slimes)


       if (_monsterCount >= maxMonsters)
       {
           _canSpawn = false;
           return;
       }
       
        GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        GameObject spawnedMonster = Instantiate(monsterPrefab);
        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();
        
        if (!monsterController) return;
        
        monsterController.InitializeMonster(mainGrid, playerHealth);

        _monsterCount++;

        monsterManager.DetectBicho(monsterController);
        
        _spawnTimer = firstSpawnTime;


    }
    
    
}
