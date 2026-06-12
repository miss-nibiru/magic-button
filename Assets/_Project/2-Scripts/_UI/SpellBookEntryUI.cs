using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls one spell banner in the spellbook.
/// </summary>
public class SpellBookEntryUI : MonoBehaviour
{
    [SerializeField] private Image spellBanner;
    [SerializeField] private Image deactivatedOverlay;

    [SerializeField] private float overlayAlpha = 0.8f;
    [SerializeField] private float flashTime = 0.17f;
    [SerializeField] private float pulseTime = 0.15f;
    [SerializeField] private float pulseScale = 1.1f;

    private SpellData _spell;
    private Coroutine _flashCoroutine;
    private Coroutine _pulseCoroutine;
    private Vector3 _normalScale;
    private Outline _outline;

    public SpellData Spell => _spell;

    private void Awake()
    {
        _normalScale = transform.localScale;

        if (spellBanner)
        {
            _outline = spellBanner.GetComponent<Outline>();
        }

        if (_outline) _outline.enabled = false;
    }

    public void SetSpell(SpellData spell)
    {
        _spell = spell;

        if (spellBanner) spellBanner.sprite = spell.SpellBookBanner;

        ResetBannerColor();

        if (_outline) _outline.enabled = false;
    }
    
    public void SetUseful(bool isUseful)
    {
        if (!deactivatedOverlay) return;

        deactivatedOverlay.gameObject.SetActive(!isUseful);

        Color overlayColor = deactivatedOverlay.color;
        overlayColor.a = overlayAlpha;
        deactivatedOverlay.color = overlayColor;
    }

    public void PlaySuccessReaction()
    {
        if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
        _pulseCoroutine = StartCoroutine(PulseRoutine());
    }

    public void FlashRed()
    {
        if (!spellBanner) return;

        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator PulseRoutine()
    {
        if (_outline) _outline.enabled = true;

        transform.localScale = _normalScale * pulseScale;

        yield return new WaitForSeconds(pulseTime);

        transform.localScale = _normalScale;

        yield return new WaitForSeconds(pulseTime);

        if (_outline) _outline.enabled = false;
    }

    private IEnumerator FlashRedRoutine()
    {
        spellBanner.color = new Color(1f, 0.25f, 0.25f, 1f);

        yield return new WaitForSeconds(flashTime);

        ResetBannerColor();
    }

    private void ResetBannerColor()
    {
        if (!spellBanner) return;

        Color bannerColor = Color.white;
        bannerColor.a = 1f;
        spellBanner.color = bannerColor;
    }

    [ContextMenu("Test Success Reaction")]
    private void TestSuccessReaction()
    {
        PlaySuccessReaction();
    }

    [ContextMenu("Test Red Flash")]
    private void TestRedFlash()
    {
        FlashRed();
    }
}