using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class CollectableTracker : ScriptableObject
{
    public string StoredSceneName;

    public int CurrentLevelEnemyCount;
    public int CurrentLevelEnemies_destroyed;
    public int TotalEnemies;
    public int TotalEnemiesDestroyed;

    public void StoreCurrentScene(string currentSceneName)
    {
        Debug.Log($"Stored scene name: {StoredSceneName}");
        Debug.Log($"Sent scene name: {currentSceneName}");

        if (currentSceneName != StoredSceneName)
        {
            StoredSceneName = currentSceneName;
            Debug.Log($"Scene changed to {StoredSceneName}");

        }
        else if(currentSceneName == StoredSceneName)
        {
            Debug.Log($"Current scene: {currentSceneName}");
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
        TotalEnemies += CurrentLevelEnemyCount;
    }


}
