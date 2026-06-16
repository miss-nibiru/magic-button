using UnityEngine;

/// <summary>
/// scriptable object that holds the data for each monster
/// different monsters will have different spells they are weak to
/// </summary>

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monsters/Monster Data")]
public class MonsterData : ScriptableObject
{
    
    [SerializeField] private string monsterName;
    [SerializeField] private SpellData monsterWeakness;
    [SerializeField] private float monsterSpeed;
    [SerializeField] private int monsterDamage;
    
    [SerializeField] private float monsterSpawnDelay;
    [SerializeField] private int maxActive;
    [SerializeField] private int minTypeSpacing;
    [SerializeField] private int maxTypeSpacing;
    [SerializeField] private bool randomSpacing;
    
    [SerializeField] private int startingColumn;
    [SerializeField] private int dangerZone;
    
    public string MonsterName => monsterName;
    public SpellData MonsterWeakness => monsterWeakness;
    public float MonsterSpeed => monsterSpeed;
    public int MonsterDamage => monsterDamage;
    
    public float MonsterSpawnDelay => monsterSpawnDelay;
    public int MaxActive => maxActive;
    public int MinTypeSpacing => minTypeSpacing;
    public int MaxTypeSpacing => maxTypeSpacing;
    public bool RandomSpacing => randomSpacing;
    public int StartingColumn => startingColumn;
    public int DangerZone => dangerZone;
    
}
