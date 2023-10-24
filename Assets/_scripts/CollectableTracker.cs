using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class CollectableTracker : ScriptableObject
{
    public string StoredSceneName;
    public bool NewScene;

    public int CurrentLevelEnemyCount;
    public int CurrentLevelEnemies_destroyed;
    public int TotalEnemies;
    public int TotalEnemiesDestroyed;

    public void CheckSceneNew(string currentSceneName)
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
        Debug.Log($"Stored scene name: {StoredSceneName}");
        Debug.Log($"Sent scene name: {currentSceneName}");

        if (NewScene)
        {
            StoredSceneName = currentSceneName;
            Debug.Log($"Scene changed to {StoredSceneName}");
        }
        else if (!NewScene)
        {
            Debug.Log($"Current scene: {currentSceneName}");
        }
    }

    public void AddToTotals()
    {
        if (NewScene)
        {
            TotalEnemies += CurrentLevelEnemyCount;
            TotalEnemiesDestroyed += CurrentLevelEnemies_destroyed;
        }
    }

    public void ResetAllTrackers()
    {
        CurrentLevelEnemyCount = 0;
        CurrentLevelEnemies_destroyed = 0;
        TotalEnemies = 0;
        TotalEnemiesDestroyed = 0;
    }

    public void ResetCurrentLevelEnemyCount() => CurrentLevelEnemyCount = 0;
    public void ResetCurrentLevelEnemiesDestroyed() => CurrentLevelEnemies_destroyed = 0;

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
            TotalEnemiesDestroyed += CurrentLevelEnemies_destroyed;
        }
    }



}
