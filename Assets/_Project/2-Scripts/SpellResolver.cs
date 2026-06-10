using UnityEngine;

/// <summary>
/// this guy receives the info from spell data and check what spell it is
/// doesnt do anything else, this damn potato
/// </summary>
public class SpellResolver : MonoBehaviour
{
   
   [SerializeField] private SpellData[] availableSpells;

   public SpellData ResolveSpell(string pattern)
   {
      foreach (SpellData spell in availableSpells)
      {
         if (spell.SpellPattern == pattern)
         {
            Debug.Log("Resolved spell: " + spell.SpellName);
            return spell;
         }
      }

      Debug.Log("No spell found for pattern: " + pattern);
      return null; 
   }
   
   
      
}
