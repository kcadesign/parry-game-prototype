using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackEnemies : MonoBehaviour
{
    public static event Action<int, int> OnGetEnemyCount;

    public CollectableTracker CollectableTracker;

    private string _enemyTag = "Enemy";

    private int _initialEnemyCount;
    private int _currentEnemyCount;

    private int _currentEnemiesDestroyed;

    private void Awake()
    {
        CollectableTracker.NewScene = false;
        //CollectableTracker.StoredSceneName = "NOT SET";
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_onSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_onSceneLoaded;
    }

    private void SceneManager_onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CollectableTracker.CheckIfSceneChanged(scene.name);
        CollectableTracker.StoreScene(scene.name);

        _initialEnemyCount = CountTaggedObjectsInScene(_enemyTag);
        CollectableTracker.CurrentLevelEnemyCount = _initialEnemyCount;

        if (CollectableTracker.NewScene)
        {
            CollectableTracker.AddCurrentEnemiesToTotal();
            CollectableTracker.AddCurrentDestroyedEnemiesToTotal();
        }
    }

    private void Update()
    {
        _currentEnemyCount = CountTaggedObjectsInScene(_enemyTag);

        CalculateEnemiesDestroyed();

        CollectableTracker.CurrentLevelEnemiesDestroyed = _currentEnemiesDestroyed;

        OnGetEnemyCount?.Invoke(_currentEnemiesDestroyed, _initialEnemyCount);

    }

    private int CountTaggedObjectsInScene(string tag)
    {
        GameObject[] taggedObjectArray;
        int enemyCount;

        taggedObjectArray = GameObject.FindGameObjectsWithTag(tag);
        enemyCount = taggedObjectArray.Length;

        return enemyCount;
    }

    private void CalculateEnemiesDestroyed()
    {
        //Debug.Log($"Initial enemies: {_initialEnemyCount} / Current enemies: {_currentEnemyCount}");
        if(_initialEnemyCount != _currentEnemyCount)
        {
            _currentEnemiesDestroyed = _initialEnemyCount - _currentEnemyCount;
        }
    }
}
