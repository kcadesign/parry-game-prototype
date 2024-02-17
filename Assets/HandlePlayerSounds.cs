using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerSounds : MonoBehaviour
{
    public SoundCollection _playerSounds;
    private AudioSource audioSource;

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        PlayerParry.OnParryBounce += PlayerParry_OnParryBounce;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        PlayerParry.OnParryBounce -= PlayerParry_OnParryBounce;
    }

    private void Awake()
    {
        // Ensure there is an AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        if (jumping && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            AssignSetPlaySound("Jump");
        }
    }

    private void PlayerParry_OnParryBounce(bool parryBouncing)
    {
        if (parryBouncing && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            AssignSetPlaySound("ParryBounce");
        }
    }

    private void AssignSetPlaySound(string soundName)
    {
        // Find the desired Sound by its name
        Sound desiredSound = _playerSounds.FindSoundByName(soundName);

        if (desiredSound != null && desiredSound.AudioClips.Length > 0)
        {
            // Select a random AudioClip from the desired Sound
            AudioClip randomClip = desiredSound.AudioClips[Random.Range(0, desiredSound.AudioClips.Length)];

            SetSoundLevels(desiredSound);
            PlaySound(randomClip);
        }
        else
        {
            Debug.LogWarning("Sound not found or no audio clips found for the desired sound.");
        }
    }

    private void SetSoundLevels(Sound desiredSound)
    {
        // Apply settings from the Sound to the AudioSource
        audioSource.volume = desiredSound.Volume;
        audioSource.pitch = desiredSound.Pitch;
        audioSource.spatialBlend = desiredSound.SpatialBlend;
        // You can add other settings such as spatial blend here
    }

    private void PlaySound(AudioClip randomClip)
    {
        // Play the random AudioClip using the configured AudioSource
        audioSource.PlayOneShot(randomClip);
    }

}
