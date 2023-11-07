using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class CollectableTracker : ScriptableObject
{
    [Header("Scene Management")]
    public string StoredSceneName;
    public bool NewScene;
    public Dictionary<string, bool> CollectiblesDictionary;
    public bool DictionaryIsInitialised;

    [Header("Enemies")]
    public int CurrentLevelEnemyCount;
    public int CurrentLevelEnemiesDestroyed;
    public int TotalEnemies;
    public int TotalEnemiesDestroyed;

    [Header("Hostages")]
    public int TotalHostagesSaved;

    public void CheckIfSceneChanged(string currentSceneName)
    {
        if (currentSceneName != StoredSceneName)
        {
            NewScene = true;
        }
        else if(currentSceneName == StoredSceneName)
        {
            NewScene = false;
        }
    }

    public void StoreScene(string currentSceneName)
    {
        //Debug.Log($"Stored scene name: {StoredSceneName}");
        //Debug.Log($"Sent scene name: {currentSceneName}");

        if (NewScene)
        {
            StoredSceneName = currentSceneName;
            Debug.Log($"New scene: {StoredSceneName}");
        }
        else if (!NewScene)
        {
            Debug.Log($"Same scene: {currentSceneName}");
        }
    }

    public void AddToTotals()
    {
        if (NewScene)
        {
            TotalEnemies += CurrentLevelEnemyCount;
            TotalEnemiesDestroyed += CurrentLevelEnemiesDestroyed;
        }
    }

    public void ResetAllTrackers()
    {
        StoredSceneName = null;

        CurrentLevelEnemyCount = 0;
        CurrentLevelEnemiesDestroyed = 0;
        TotalEnemies = 0;
        TotalEnemiesDestroyed = 0;

        TotalHostagesSaved = 0;

        ResetCollectiblesDictionary();
    }

    public void ResetCurrentLevelEnemyCount() => CurrentLevelEnemyCount = 0;
    public void ResetCurrentLevelEnemiesDestroyed() => CurrentLevelEnemiesDestroyed = 0;

    public void AddCurrentEnemiesToTotal()
    {
        if (NewScene)
        {
            TotalEnemies += CurrentLevelEnemyCount;
        }
    }

    public void AddCurrentDestroyedEnemiesToTotal()
    {
        if (NewScene)
        {
            TotalEnemiesDestroyed += CurrentLevelEnemiesDestroyed;
        }
    }

    public void IncrementHostagesSaved()
    {
        TotalHostagesSaved++;
    }

    public void InitializeCollectiblesDictionary()
    {
        // Check for initialisation so it can only be done once
        if (!DictionaryIsInitialised)
        {
            CollectiblesDictionary = new Dictionary<string, bool>();

            // Get the total number of scenes in the build index
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            // Iterate through the scenes in the build index and add their names to the Dictionary with values initialized to false
            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                // Use the scene name as the key and initialize the value to false
                CollectiblesDictionary[sceneName] = false;
            }

            DictionaryIsInitialised = true;
        }

        // Print both keys and values to the console
        foreach (var keyValuePair in CollectiblesDictionary)
        {
            Debug.Log("Key: " + keyValuePair.Key + ", Value: " + keyValuePair.Value);
        }

    }

    public void ResetCollectiblesDictionary()
    {
        DictionaryIsInitialised = false;

        foreach (var key in CollectiblesDictionary.Keys)
        {
            CollectiblesDictionary[key] = false;
        }
    }

}
