using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the ribbon that appears between waves that will have its text changing controlled by the state machine that pops the waves
/// </summary>
public class WaveRibbonUI : MonoBehaviour
{
    [SerializeField] private GameObject ribbonObject;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private float showTime = 1.5f;

    private Coroutine _ribbonCoroutine;

    private void Start()
    {
        HideRibbon();
    }

    public void ShowWaveRibbon(string message)
    {
        if (_ribbonCoroutine != null)
        {
            StopCoroutine(_ribbonCoroutine);
        }

        _ribbonCoroutine = StartCoroutine(ShowRibbonRoutine(message));
    }

    private IEnumerator ShowRibbonRoutine(string message)
    {
        ribbonObject.SetActive(true);
        waveText.text = message;

        yield return new WaitForSeconds(showTime);

        HideRibbon();

        _ribbonCoroutine = null;
    }

    private void HideRibbon()
    {
        ribbonObject.SetActive(false);
    }
}