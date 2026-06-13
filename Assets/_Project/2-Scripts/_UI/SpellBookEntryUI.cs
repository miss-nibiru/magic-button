using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls one spell banner in the spellbook.
/// </summary>
public class SpellBookEntryUI : MonoBehaviour
{
    //VISUALS IMAGES
    [SerializeField] private Image spellBanner;
    [SerializeField] private Image deactivatedOverlay;
    [SerializeField] private float overlayAlpha = 0.8f;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material correctFeedbackMaterial;
    [SerializeField] private Material failFeedbackMaterial;

    //FEEDBACK
    [SerializeField] private float feedbackTime = 0.2f;
    [SerializeField] private float pulseScale = 1.1f;

    private SpellData _spell;
    private Coroutine _feedbackCoroutine;
    private Vector3 _normalScale;

    public SpellData Spell => _spell;

    private void Awake()
    {
        _normalScale = transform.localScale;

        if (spellBanner)
        {
            normalMaterial = spellBanner.material;
        }
    }

    public void SetSpell(SpellData spell)
    {
        _spell = spell;

        if (spellBanner && spell)
        {
            spellBanner.sprite = spell.SpellBookBanner;
        }

        ResetBannerVisuals();
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
        PlayFeedback(correctFeedbackMaterial);
    }

    public void PlayFailEffect()
    {
        PlayFeedback(failFeedbackMaterial);
    }

    private void PlayFeedback(Material feedbackMaterial)
    {
        if (!spellBanner) return;

        if (_feedbackCoroutine != null)
        {
            StopCoroutine(_feedbackCoroutine);
        }

        _feedbackCoroutine = StartCoroutine(FeedbackRoutine(feedbackMaterial));
    }

    private IEnumerator FeedbackRoutine(Material feedbackMaterial)
    {
        transform.localScale = _normalScale * pulseScale;

        if (feedbackMaterial)
        {
            spellBanner.material = feedbackMaterial;
        }

        yield return new WaitForSeconds(feedbackTime);

        ResetBannerVisuals();

        _feedbackCoroutine = null;
    }

    private void ResetBannerVisuals()
    {
        transform.localScale = _normalScale;

        if (!spellBanner) return;

        spellBanner.material = normalMaterial;
        spellBanner.color = Color.white;
    }
    
}