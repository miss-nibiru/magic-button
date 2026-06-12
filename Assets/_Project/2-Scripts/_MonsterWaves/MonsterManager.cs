using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// register when the monster spawn and also removes the monster when dies or reaches the player
/// receives the spell and checks all existing monsters
/// defeat the first detected that can be defeated with that spell
/// </summary>
public class MonsterManager : MonoBehaviour
{
    
    private List<MonsterController> activeMonsters = new List<MonsterController>(); // heres the list of all thingys
    
    public void DetectBicho(MonsterController monster)
    {
        
        activeMonsters.Add(monster);
        Debug.Log("Detected Monster " + monster.name);
        
    }

    public int ActiveMonsterCount
    {
        get
        {
            activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);
            return activeMonsters.Count;
        }
    }

    public bool DefeatwithSpell(SpellData spell)
    {
        activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);

        for (int i = 0; i < activeMonsters.Count; i++)
        {
            MonsterController monster = activeMonsters[i];

            bool monsterDefeated = monster.DefeatMonsterwithSpell(spell);

            if (monsterDefeated)
            {
                activeMonsters.RemoveAt(i);
                return true;
            }
        }

        return false;
        
    }

    public MonsterController FindCorrectTarget(SpellData spell)
    {
        activeMonsters.RemoveAll(monster => !monster || !monster.CanBeTargeted);

        for (int i = 0; i < activeMonsters.Count; i++)
        {
            MonsterController monster = activeMonsters[i];

            if (monster.IsWeakTo(spell))
            {
                return monster;
            }
            
        }
    
        return null;
        
    }
    
    
}
