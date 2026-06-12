using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls one spell banner shows on the spellbook adn the main spell is visible underneath an overlay that appears when the spell is not needed
/// </summary>
public class SpellBookEntryUI : MonoBehaviour
{
    [SerializeField] private Image spellBanner;
    [SerializeField] private Image deactivatedOverlay;

    [SerializeField] private float overlayAlpha;

    private SpellData _spell;

    public SpellData Spell => _spell;

    public void SetSpell(SpellData spell)
    {
        _spell = spell;

        if (spellBanner && spell.SpellBookBanner)
        {
            spellBanner.sprite = spell.SpellBookBanner;
        }

        if (spellBanner)
        {
            Color bannerColor = spellBanner.color;
            bannerColor.a = 1f;
            spellBanner.color = bannerColor;
        }
    }
    
    public void SetUseful(bool isUseful)
    {
        if (!deactivatedOverlay) return;

        deactivatedOverlay.gameObject.SetActive(!isUseful);

        Color overlayColor = deactivatedOverlay.color;
        overlayColor.a = overlayAlpha;
        deactivatedOverlay.color = overlayColor;
    }
}