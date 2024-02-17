using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundCollection : ScriptableObject
{
    public Sound[] Sounds;

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
