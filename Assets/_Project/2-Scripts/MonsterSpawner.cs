using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knows what monsters exists and spawns them on the grid
/// Each mosnter knows what column to spawn in, the spawner just spawns them
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private MainGrid mainGrid;
    [SerializeField] private PlayerHealth playerHealth;
    
    private bool _canSpawn;

    private void Start()
    {
        SpawnMonster();
    }
    
    //pick a prefab from the pool, instantiate it, 

    private void SpawnMonster()
    {
       // if (//the game state is wave 1, then the player gets only green slimes)
       
        GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        GameObject spawnedMonster = Instantiate(monsterPrefab);
        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();
        
        if (!monsterController) return;
        
        monsterController.InitializeMonster(mainGrid, playerHealth);
        

    }
    
    
}
