using UnityEngine;

/// <summary>
/// Controls the player's health and takes damage.
/// </summary>
/// 
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerAudio playerAudio;
    
    [SerializeField] private GameObject[] fullHearts;
    [SerializeField] private GameObject[] emptyHearts;
    [SerializeField] private int maxHealth;
    
    private int _currentHealth;
    private bool _isDead;
    
    private void Start()
    {
        _currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    public void TakeDamage(int damageAmount)
    {
        if (_isDead) return;

        _currentHealth -= damageAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        if (playerAudio) playerAudio.PlayPlayerHitSound();
        UpdateHeartsUI();

        if (_currentHealth <= 0) DieBish();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < fullHearts.Length; i++)
        {
            bool shouldBeFull = i < _currentHealth;

            fullHearts[i].SetActive(shouldBeFull);
            emptyHearts[i].SetActive(!shouldBeFull);
        }
    }
    
    public void DieBish()
    {
        _isDead = true;
        Debug.Log("Player is dead");
        
        gameManager.BishIsDead();
        
    }
    
    // public void Heal(int healAmount) -- add if theres time
    // {
    //     _currentHealth += healAmount;
    // }
    
}