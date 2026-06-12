using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To control how each spell in the spell ui thingy readct depedning on what the player manages to complete and dont.
/// inputted spell not availab,e flash red and take damage?
/// </summary>
public class SpellBookEntryUI : MonoBehaviour
{
    [SerializeField] private Image spellBanner;
    [SerializeField] private Image deactivatedOverlay;
    [SerializeField] private ParticleSystem clickParticle;

    [SerializeField] private float overlayAlpha;
    [SerializeField] private float flashTime;
    [SerializeField] private float pulseTime;
    [SerializeField] private float pulseScale;

    private SpellData _spell;
    private Coroutine _flashCoroutine;
    private Coroutine _pulseCoroutine;
    private Vector3 _normalScale;

    public SpellData Spell => _spell;

    private void Awake()
    {
        _normalScale = transform.localScale;
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
    
    public void SetSpell(SpellData spell)
    {
        _spell = spell;

        if (spellBanner) spellBanner.sprite = spell.SpellBookBanner;
        if (clickParticle) clickParticle.Stop();

        ResetBannerColor();
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
        if (clickParticle)
        {
            clickParticle.Clear();
            clickParticle.Play();
        }

        if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
        _pulseCoroutine = StartCoroutine(PulseRoutine());
    }

    public void FlashRed()
    {
        if (!spellBanner) return;

        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator FlashRedRoutine()
    {
        spellBanner.color = new Color(1f, 0.2f, 0.2f, 1f);

        yield return new WaitForSeconds(flashTime);

        ResetBannerColor();
    }

    private IEnumerator PulseRoutine()
    {
        transform.localScale = _normalScale * pulseScale;

        yield return new WaitForSeconds(pulseTime);

        transform.localScale = _normalScale;
    }

    private void ResetBannerColor()
    {
        if (!spellBanner) return;

        Color bannerColor = Color.white;
        bannerColor.a = 1f;
        spellBanner.color = bannerColor;
    }
}