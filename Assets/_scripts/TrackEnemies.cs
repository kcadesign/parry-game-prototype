using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackEnemies : MonoBehaviour
{
    public static event Action<int> OnGetEnemyCount;

    public CollectableTracker CollectableTracker;

    //private string _currentSceneName;

    private string _enemyTag = "Enemy";
    //private GameObject[] _taggedObjectArray;

    private int _initialEnemyCount;
    private int _currentLevelEnemyCount;

    private void Awake()
    {
        CollectableTracker.CheckSceneNew(SceneManager.GetActiveScene().name);
        CollectableTracker.StoreScene(SceneManager.GetActiveScene().name);

        CollectableTracker.ResetCurrentLevelEnemiesDestroyed();
        _initialEnemyCount = CountTaggedObjectsInScene(_enemyTag);
        CollectableTracker.CurrentLevelEnemyCount = _initialEnemyCount;
        CollectableTracker.AddCurrentEnemiesToTotal();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_onSceneLoaded;
    }


    private void OnDisable()
    {
        //CollectableTracker.ResetTracker();
        SceneManager.sceneLoaded -= SceneManager_onSceneLoaded;

        CollectableTracker.AddCurrentDestroyedEnemiesToTotal();
    }

    private void SceneManager_onSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    
    private void Update()
    {
        _currentLevelEnemyCount = CountTaggedObjectsInScene(_enemyTag);
        CollectableTracker.CurrentLevelEnemies_destroyed = CalculateEnemiesDestroyed();
    }

    private int CountTaggedObjectsInScene(string tag)
    {
        GameObject[] taggedObjectArray;
        int enemyCount;

        taggedObjectArray = GameObject.FindGameObjectsWithTag(tag);
        enemyCount = taggedObjectArray.Length;


        return enemyCount;
    }

    private int CalculateEnemiesDestroyed()
    {
        int enemiesDestroyed;
        enemiesDestroyed = _initialEnemyCount - _currentLevelEnemyCount;

        OnGetEnemyCount?.Invoke(enemiesDestroyed);

        return enemiesDestroyed;

    }
}
