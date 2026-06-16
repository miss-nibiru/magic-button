using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// register when the monster spawn and also removes the monster when dies or reaches the player
/// receives the spell and checks all existing monsters
/// defeat the first detected that can be defeated with that spell
/// </summary>
public class MonsterManager : MonoBehaviour
{
    
    private List<MonsterController> _activeMonsters = new List<MonsterController>(); // heres the list of all thingys


    public bool MonsterInColumn(int column)
    {
        _activeMonsters.RemoveAll(monster => !monster.CanBeTargeted);

        foreach (MonsterController monster in _activeMonsters)
            if (monster.CurrentColumn == column) return true;
        
        return false;
        
    }
    public void DetectBicho(MonsterController monster)
    {
        _activeMonsters.Add(monster);
        
    }

    public int ActiveMonsterCount
    {
        get
        {
            _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);
            return _activeMonsters.Count;
        }
    }

    public int CountActiveMonsters( MonsterData monsterData)
    {
        if (!monsterData) return 0;
        
        _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);

        int count = 0;
        
        foreach (MonsterController monster in _activeMonsters)
        {
            if (!monster) continue;
            if (monster.MonsterData == monsterData) count++;
            
        }
        
        return count;

    }

    public bool CanSpawnType(MonsterData monsterData)
    {
        if (!monsterData) return false;

        int activeMonsterCount = CountActiveMonsters(monsterData);
            
            if (activeMonsterCount >= monsterData.MaxActive)
                return false;
            
            return true;
        
    }
    
    public bool CanSpawnAtColumn(MonsterData monsterData, int spawnColumn)
    {
        if (!monsterData) return false;

        _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);

        int activeAmount = CountActiveMonsters(monsterData);

        if (monsterData.MaxActive > 0 && activeAmount >= monsterData.MaxActive) return false;

        if (MonsterInColumn(spawnColumn)) return false;

        foreach (MonsterController monster in _activeMonsters)
        {
            if (!monster) continue;
            if (monster.MonsterData != monsterData) continue;

            int distance = Mathf.Abs(monster.CurrentColumn - spawnColumn);
            if (distance < monsterData.MinTypeSpacing) return false;
            if (monsterData.MaxTypeSpacing > 0 && distance > monsterData.MaxTypeSpacing)  return false;
        }

        return true;
    }

    public bool HaveGoodSpacing(MonsterData monsterData, int spawnColumn)
    {
        if (!monsterData) return false;

        _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);
        foreach (MonsterController monster in _activeMonsters)
        {
            if (!monster) continue;

            if (monster.MonsterData != monsterData) continue;

            int distance = Mathf.Abs(monster.CurrentColumn - spawnColumn);

            if (distance < monsterData.MinTypeSpacing) return false;
        }

        return true;
    }

    public MonsterController FindCorrectTarget(SpellData spell)
    {
        _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);

        for (int i = 0; i < _activeMonsters.Count; i++)
        {
            MonsterController monster = _activeMonsters[i];

            if (monster.IsWeakTo(spell)) return monster;
            
        }
    
        return null;
        
    }

    public void PushAllBack()
    {
        _activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);
        foreach (MonsterController monster in _activeMonsters)
            monster.PushAfterDamaging();
        
    }
    
    
}