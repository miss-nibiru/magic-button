using UnityEngine;
/// <summary>
/// controls what is the spell button
/// checks when the button was pressed and released for hold and tap
/// </summary>
public class PlayerSpellsInputs : MonoBehaviour
{
    [SerializeField] private PlayerSpellCasting spellCast;
    [SerializeField] private KeyCode spellButton; // dude i found this and its rough but super easy ill just this for prototypes forever now
    [SerializeField] private float holdThreshold; // how long the button needs to be held to be considered a hold instead of a tap
    
    [SerializeField] private SpellResolver spellResolver; // reference to the spell resolver
    [SerializeField] private float spellFinishDelay;
    
    private string _currentSpellPattern; // store the current spell pattern as the player inputs it
    private string _currentSpellName;
    private float _finishTimer;
    private bool _buildingSpell; //stores when the player is still buildign the spell
    
    private float _buttonPressTime;
    private bool _isHolding;

    private void Update()
    {
        ButtonPressed();

        if (_buildingSpell && !_isHolding)
        {
            _finishTimer -= Time.deltaTime;

            if (_finishTimer <= 0)
            {
                SpellData resolvedSpell = spellResolver.ResolveSpell(_currentSpellPattern);

                if (resolvedSpell != null)
                {
                    spellCast.CastSpell(resolvedSpell);
                    Debug.Log("Player cast: " + resolvedSpell.SpellName);
                }
                else
                {
                    Debug.Log("Player failed to cast a valid spell");
                    spellCast.FailSpell();
                }

                _currentSpellPattern = "";
                _buildingSpell = false;

                Debug.Log("spell finished");
            }
        }
    }
    
    private void ButtonPressed()
    {
        if (Input.GetKeyDown(spellButton))
        {
           _buttonPressTime = Time.time;
           _isHolding = true;
           
           Debug.Log("spell button pressed");
        }

        if (Input.GetKeyUp(spellButton)) // when the button is released check how long it was held
        {
            float heldTime = Time.time - _buttonPressTime;
            _isHolding = false;
            
            if (heldTime >= holdThreshold)
            {
                _currentSpellPattern += "-";
                _finishTimer = spellFinishDelay;
                _buildingSpell = true;
                Debug.Log("HOLD");
            }
            
            else
            {
                _currentSpellPattern += ".";
                _finishTimer = spellFinishDelay; 
                _buildingSpell = true;
                Debug.Log("TAP");
            }
            
            Debug.Log("Player cast the spell" + _currentSpellPattern + _currentSpellName);
            
        }
        
    }
    
}
