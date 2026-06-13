using UnityEngine;
using System.Collections;

/// <summary>
/// I think this script is necessary to pass the final information into the game
/// player inputs then the spell gets resoved then the spell is cast is my thinking process so thats 3 scripts. Fight me.
///
/// this also handles the spells projectiles and how the move down the grid
/// detects the monsters - projectile reaches to the thing and kills it
/// </summary>
public class PlayerSpellCasting : MonoBehaviour
{
    [SerializeField] private SpellTextUI spellText;
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private SpellBookUI spellBookUI;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private SpellProjectile spellProjectile;
    [SerializeField] private Transform projectileSpawn;
    
    //player gets confused or dizzy if they miss a spell instead of losing hp

    [SerializeField] private float dizzyCoolDown;

    private bool _isDizzy;
    private Coroutine _dizzyCoroutine;

    //this script can receive a resolved spell, then tell the monster what spell is and if thats weak, if the spell is wrong, it damages the player

    public void CastSpell(SpellData spell)
    {

        if (!spell) return;
        if (_isDizzy) return;

        MonsterController target = monsterManager.FindCorrectTarget(spell); // finds the monster that can be killed

        if (!target)
        {

            if (spellBookUI) spellBookUI.ShowFailedSpellFeedback(spell);
            if (spellText) spellText.ShowDizzyBish();

            StartDizzyCooldown();
            return;
        }
        
        if (spellBookUI) spellBookUI.ShowSuccessForSpell(spell);
        if (spellText) spellText.ShowSpellName(spell);
        
        if (playerAudio) playerAudio.PlaySpellCastSound(); playerAudio.PlayPlayerCastSound();

        SpellProjectile projectile = Instantiate(
            spellProjectile,
            projectileSpawn.position,
            Quaternion.identity
        );

        projectile.Initialize(spell, target);
    }

    private void StartDizzyCooldown()
    {
        
        if (_dizzyCoroutine != null) StopCoroutine(_dizzyCoroutine);

        _dizzyCoroutine = StartCoroutine(DizzyRoutine());

    }

    private IEnumerator DizzyRoutine()
    {
        _isDizzy = true;
        Debug.Log("Player SpellCasting Dizzy"); // need ui to show this in the future
        
        yield return new WaitForSeconds(dizzyCoolDown);
        
        _isDizzy = false;
        _dizzyCoroutine = null;
        
        Debug.Log("Player not dizzy anymore");
        
    }

    public void FailSpell()
    {
        playerHealth.TakeDamage(1);
    }
    
    
}
