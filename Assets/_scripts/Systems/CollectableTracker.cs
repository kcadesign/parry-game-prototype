using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class CollectableTracker : ScriptableObject
{
    public string StoredSceneName;
    public bool NewScene;

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

}
