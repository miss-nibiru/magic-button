using UnityEngine;

/// <summary>
/// I think this script is necessary to pass the final information into the game
/// player inputs then the spell gets resoved then the spell is cast is my thinking process so thats 3 scripts. Fight me.
/// </summary>
public class PlayerSpellCasting : MonoBehaviour
{

    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private PlayerHealth playerHealth;
    
    
    
    //this script can receive a resolved spell, then tell the monster what spell is and if thats weak, if the spell is wrong, it damages the player

    public void CastSpell(SpellData spell)
    {

        if (!spell) return;
        if (!monsterManager) return;

        bool successSpell = monsterManager.DefeatwithSpell(spell);
        
        if (!successSpell) playerHealth.TakeDamage(1);


    }
    
}
