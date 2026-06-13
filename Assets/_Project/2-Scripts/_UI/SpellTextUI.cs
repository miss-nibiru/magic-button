using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// This script handles the text that is show to the player to show what spell is being casted
/// When a spell is casted properly it changes to show the name of the spell
/// if a spell casted is incorrect the text changes to "oof-im dizzy!"
/// </summary>
public class SpellTextUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text spellUIText;
    [SerializeField] private string defaultMessage;
    [SerializeField] private string dizzyMessage;

    [SerializeField] private float dizzyDuration;
    [SerializeField] private bool textLocked;
    
    private Coroutine _textLockCoroutine;
    

    private void Start()
    {
        ClearText();
    }

    public void ShowSpellName(SpellData spell)
    {
        if (!spellUIText) return;
        if (!spell) return;
        if (textLocked) return;

        spellUIText.text = spell.SpellName;

    }
    
    public void ShowSpellPattern(string pattern)
    {
        if (!spellUIText) return;
        if (textLocked) return;

        spellUIText.text = "<size=200%>" + pattern + "</size>";
    }

    public void ShowDizzyBish()
    {
        if (!spellUIText) return;

        if (_textLockCoroutine != null) StopCoroutine(_textLockCoroutine);
        
        _textLockCoroutine = StartCoroutine(TextLockCoroutine());
    }

    private IEnumerator TextLockCoroutine()
    {
        textLocked = true;
        spellUIText.text = dizzyMessage;
        yield return new WaitForSeconds(dizzyDuration);
        
        textLocked = false;
        _textLockCoroutine = null;
        
        ClearText();
        
    }

    public void ClearText()
    {
        if (!spellUIText) return;
        spellUIText.text = defaultMessage;
    }

}
