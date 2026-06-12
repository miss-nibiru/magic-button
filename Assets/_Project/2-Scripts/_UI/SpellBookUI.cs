using UnityEngine;

/// <summary>
/// contrgols the spellbook ui on runtime so it shows the spells that are available for that wave
/// </summary>
public class SpellBookUI : MonoBehaviour
{
    [SerializeField] private SpellData[] allSpells;
    [SerializeField] private SpellBookEntryUI[] spellEntries;

    private void Awake()
    {
        BuildSpellBook();
    }

    private void BuildSpellBook()
    {
        for (int i = 0; i < spellEntries.Length; i++)
        {
            if (!spellEntries[i]) continue;

            if (i >= allSpells.Length || !allSpells[i])
            {
                spellEntries[i].SetUseful(false);
                continue;
            }

            spellEntries[i].SetSpell(allSpells[i]);
            spellEntries[i].SetUseful(true);
        }
    }

    public void ShowUsefulSpells(SpellData[] neededSpells)
    {
        Debug.Log("SpellBookUI updated useful spells");

        for (int i = 0; i < spellEntries.Length; i++)
        {
            SpellBookEntryUI entry = spellEntries[i];

            if (!entry) continue;

            bool isNeeded = IsSpellNeeded(entry.Spell, neededSpells);
            entry.SetUseful(isNeeded);
        }
    }

    private bool IsSpellNeeded(SpellData spell, SpellData[] neededSpells)
    {
        if (!spell) return false;

        for (int i = 0; i < neededSpells.Length; i++)
        {
            if (!neededSpells[i]) continue;

            if (spell == neededSpells[i])
            {
                return true;
            }
        }

        return false;
    }
    
    
}