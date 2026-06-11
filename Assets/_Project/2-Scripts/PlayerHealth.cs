using UnityEngine;
/// <summary>
/// controls the player's health and takes damage
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private GameObject[] fullHearts;
    [SerializeField] private GameObject[] emptyHearts;
    
    [SerializeField] private int maxHealth;
    
    private int _currentHealth;
    private bool _canTakeDamage;
    private bool _isDead;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        //take different damage per MonsterData 
        
        _currentHealth = _currentHealth - damageAmount;
        Debug.Log("player took damage: " + damageAmount + " health remaining: " + _currentHealth);
        
        
        if (_currentHealth <= 0)
        {
            DieBish();
        }
        
    }
    
    public void DieBish()
    {
        
        _isDead = true;
        _canTakeDamage = false;
        Debug.Log("Player is dead");
        
    }
    
}
