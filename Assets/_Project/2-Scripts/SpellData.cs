using UnityEngine;

/// <summary>
/// scriptable object that holds the data for each spell
/// this will be used to create different spells with different effects and stats
/// </summary>

[CreateAssetMenu(fileName = "SpellData", menuName = "Spells/Spell Data")]
public class SpellData : ScriptableObject
{
    
    [SerializeField] private string spellName;
    [SerializeField] private string spellLetter;
    [SerializeField] private string spellPattern;
   
    
    public string SpellName => spellName;
    public string SpellLetter => spellLetter;
    public string SpellPattern => spellPattern;
    
    
}
