using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectableTrackerTest : ScriptableObject
{
    [Header("Lists")]
    public List<string> GameplayLevelsList = new();
    public Dictionary<string, int> EnemiesDestroyedDictionary = new();
    public Dictionary<string, bool> HostagesRescuedDictionary = new();

    [Header("Scene Management")]
    public string CurrentSceneName;
    public bool NewScene;
    //public bool LevelFinished;
    public bool HostagePresent;

    [Header("Enemies")]
    public int CurrentLevelEnemyCount;
    public int TotalEnemies;
    public int TotalEnemiesDestroyed;

    [Header("Hostages")]
    public int TotalHostages;
    public int TotalHostagesSaved;

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

    public void StoreCurrentSceneName(string currentSceneName) => CurrentSceneName = currentSceneName;

    public void AddSceneNameToList(string currentSceneName)
    {
        Debug.Log($"Enemies present: {CurrentLevelEnemyCount}");
        Debug.Log($"Scene name exists in list: {GameplayLevelsList.Contains(currentSceneName)}");

        if ((CurrentLevelEnemyCount > 0 || HostagePresent) && !GameplayLevelsList.Contains(currentSceneName))
        {
            GameplayLevelsList.Add(currentSceneName);
        }
    }
    
    public void AddCurrentEnemiesToTotal()
    {
        if (NewScene)
        {
            TotalEnemies += CurrentLevelEnemyCount;
        }
    }

    public void AddCurrentDestroyedEnemiesToTotal(int currentLevelEnemiesDestroyed)
    {
        if (NewScene)
        {
            TotalEnemiesDestroyed += currentLevelEnemiesDestroyed;
        }
    }

    public void AddHostageToTotal()
    {
        if (NewScene)
        {
            TotalHostages++;
        }
    }

    public void AddSavedHostageToTotal()
    {
        if (NewScene)
        {
            TotalHostagesSaved++;
        }
    }

    public void UpdateEnemiesDestroyedDictionary(int count)
    {
        EnemiesDestroyedDictionary[CurrentSceneName] = count;
        PrintEnemiesDestroyedDictionary();
    }

    public void UpdateHostageRescuedDictionary(bool rescued)
    {
        HostagesRescuedDictionary[CurrentSceneName] = rescued;
        PrintHostagesSavedDisctionary();
    }

    public void PrintEnemiesDestroyedDictionary()
    {
        foreach (var keyValuePair in EnemiesDestroyedDictionary)
        {
            Debug.Log($"Key: {keyValuePair.Key}, Enemies destroyed: {keyValuePair.Value}");
        }
    }

    private void PrintHostagesSavedDisctionary()
    {
        foreach (var keyValuePair in HostagesRescuedDictionary)
        {
            Debug.Log($"Key: {keyValuePair.Key}, Hostage saved: {keyValuePair.Value}");
        }
    }
}
