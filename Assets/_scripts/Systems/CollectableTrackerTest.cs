using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class CollectableTrackerTest : ScriptableObject
{
    [Header("Dictionaries")]
    public List<string> sceneNames = new();
    public Dictionary<string, int> EnemiesDestroyed = new();
    public Dictionary<string, bool> HostagesRescued = new();
    public bool DictionaryIsInitialised;

    [Header("Scene Management")]
    public string CurrentSceneName;
    public bool NewScene;


    [Header("Enemies")]
    public int CurrentLevelEnemyCount;
    public int CurrentLevelEnemiesDestroyed;
    public int TotalEnemies;
    public int TotalEnemiesDestroyed;

    [Header("Hostages")]
    public int TotalHostagesSaved;


    public void PopulateAndInitialiseDictionaries()
    {
        if (!DictionaryIsInitialised)
        {
            PopulateSceneNameList();
            InitialiseDictionaries();

            PrintDictionaries();

            DictionaryIsInitialised = true;
        }
        else
        {
            PrintDictionaries();

            return;
        }
        Debug.Log("Dictionaries initialised");

    }

    public void CheckIfSceneChanged(string currentSceneName)
    {
        if (currentSceneName != CurrentSceneName)
        {
            NewScene = true;
        }
        else if (currentSceneName == CurrentSceneName)
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
            CurrentSceneName = currentSceneName;
            Debug.Log($"New scene: {CurrentSceneName}");
        }
        else if (!NewScene)
        {
            Debug.Log($"Same scene: {currentSceneName}");
        }
    }


    public void PopulateSceneNameList()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            sceneNames.Add(sceneName);
        }
    }

    public void InitialiseDictionaries()
    {
        foreach (string sceneName in sceneNames)
        {
            EnemiesDestroyed[sceneName] = 0; // Default to 0 enemies destroyed
            HostagesRescued[sceneName] = false; // Default to no hostage rescued
        }

    }

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


    public void UpdateEnemiesDestroyed(string sceneName, int count)
    {
        if (EnemiesDestroyed.ContainsKey(sceneName))
        {
            EnemiesDestroyed[sceneName] = count;
        }
    }

    public void UpdateHostageRescued(string sceneName, bool rescued)
    {
        if (HostagesRescued.ContainsKey(sceneName))
        {
            HostagesRescued[sceneName] = rescued;
        }
    }

    public void PrintDictionaries()
    {
        // Print both keys and values to the console
        foreach (var keyValuePair in EnemiesDestroyed)
        {
            Debug.Log($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
        }

        foreach (var keyValuePair in HostagesRescued)
        {
            Debug.Log($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
        }
    }
}
