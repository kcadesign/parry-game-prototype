using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundCollection : ScriptableObject
{
    public Sound[] Sounds;

    public void PlaySound(string soundName, Transform objectTransform)
    {
        // Find the desired Sound by its name
        Sound desiredSound = FindSoundByName(soundName);

        if (desiredSound != null && desiredSound.AudioClips.Length > 0)
        {
            // Select a random AudioClip from the desired Sound
            AudioClip randomClip = desiredSound.AudioClips[Random.Range(0, desiredSound.AudioClips.Length)];

            // Create a new GameObject to play the sound
            GameObject sfxObject = new("sfxObject");
            sfxObject.transform.position = objectTransform.position;
            AudioSource audioSource = sfxObject.AddComponent<AudioSource>();

            // Apply settings from the Sound to the AudioSource
            audioSource.volume = desiredSound.Volume;
            audioSource.pitch = desiredSound.Pitch;
            audioSource.spatialBlend = desiredSound.SpatialBlend;
            audioSource.loop = desiredSound.Loop;
            // Add other settings here

            // Play the random AudioClip using the AudioSource
            audioSource.PlayOneShot(randomClip);

            // Destroy the sound GameObject after the sound has finished playing
            Destroy(sfxObject, randomClip.length);
        }
        else
        {
            Debug.LogWarning("Sound not found or no audio clips found for the desired sound.");
        }
    }

    // Method to find a Sound by its name
    public Sound FindSoundByName(string name)
    {
        foreach (Sound sound in Sounds)
        {
            if (sound.Name == name)
            {
                return sound;
            }
        }
        // If no matching Sound is found, return null
        return null;
    }

}
