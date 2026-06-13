using UnityEngine;

/// <summary>
/// Handles all kinds of player audio feedback. is this the best way to do it?
/// Should each spell carry its own audio insted?
/// </summary>
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    //VARIOUS KINDS OF AUDIO CLIPS
    [SerializeField] private AudioClip[] spellCastClips;
    [SerializeField] private AudioClip[] playerCastClips;
    [SerializeField] private AudioClip[] playerHitClips;

    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    public void PlaySpellCastSound()
    {
        if (!audioSource) return;

        if (spellCastClips == null || spellCastClips.Length == 0) return;

        int randomIndex = Random.Range(0, spellCastClips.Length);
        AudioClip randomClip = spellCastClips[randomIndex];

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(randomClip);
    }

    public void PlayPlayerCastSound()
    {

        if (!audioSource) return;

        if (playerCastClips == null || playerCastClips.Length == 0) return;

        int randomIndex = Random.Range(0, playerCastClips.Length);
        AudioClip randomClip = playerCastClips[randomIndex];

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(randomClip);
        
    }

    public void PlayPlayerHitSound()
    {
        if (!audioSource) return;

        if (playerHitClips == null || playerHitClips.Length == 0) return;

        int randomIndex = Random.Range(0, playerHitClips.Length);
        AudioClip randomClip = playerHitClips[randomIndex];

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(randomClip);
        
        
    }
    
}