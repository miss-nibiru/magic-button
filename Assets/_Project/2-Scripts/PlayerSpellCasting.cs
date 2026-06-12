using UnityEngine;

/// <summary>
/// I think this script is necessary to pass the final information into the game
/// player inputs then the spell gets resoved then the spell is cast is my thinking process so thats 3 scripts. Fight me.
///
/// this also handles the spells projectiles and how the move down the grid
/// detects the monsters - projectile reaches to the thing and kills it
/// </summary>
public class PlayerSpellCasting : MonoBehaviour
{

    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private SpellProjectile spellProjectile;
    [SerializeField] private Transform projectileSpawn;

    //this script can receive a resolved spell, then tell the monster what spell is and if thats weak, if the spell is wrong, it damages the player

    public void CastSpell(SpellData spell)
    {

        if (!spell) return;

        MonsterController target = monsterManager.FindCorrectTarget(spell);

        if (!target)
        {
            playerHealth.TakeDamage(1);
            return;
        }

        SpellProjectile projectile = Instantiate(
            spellProjectile,
            projectileSpawn.position,
            Quaternion.identity
        );

        projectile.Initialize(spell, target);
    }

    public void FailSpell()
    {
        playerHealth.TakeDamage(1);
    }
    
    
}
