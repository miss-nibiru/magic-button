using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// register when the monster spawn and also removes the monster when dies or reaches the player
/// receives the spell and checks all existing monsters
/// defeat the first detected that can be defeated with that spell
/// </summary>
public class MonsterManager : MonoBehaviour
{
    
    [SerializeField] private PlayerHealth playerHealth;
    private List<MonsterController> activeMonsters = new List<MonsterController>(); // heres the list of all thingys


    public void DetectBicho(MonsterController monster)
    {
        
        activeMonsters.Add(monster);
        Debug.Log("Detected Monster " + monster.name);
        
    }

    public bool DefeatwithSpell(SpellData spell)
    {

        for (int i = activeMonsters.Count - 1; i >= 0; i--)
        {
            MonsterController monster = activeMonsters[i];

            if (!monster)
            {
                activeMonsters.RemoveAt(i);
            }

            bool monsterDefeated = monster.DefeatMonsterwithSpell(spell);

            if (monsterDefeated)
            {
                activeMonsters.RemoveAt(i);
                return true;
            }
            
        }
        
        return false;
        
    }
    
}
