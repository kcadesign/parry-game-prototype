using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackEnemies : MonoBehaviour
{
    public static event Action<int, int> OnGetEnemyCount;

    public CollectableTrackerTest CollectableTrackerTest;
    public TrackScene SceneTracker;

    private string _enemyTag = "Enemy";

    private int _initialEnemyCount;
    private int _currentEnemyCount;

    private int _currentEnemiesDestroyed;

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
        _initialEnemyCount = CountTaggedObjectsInScene(_enemyTag);
        CollectableTrackerTest.CurrentLevelEnemyCount = _initialEnemyCount;

        CollectableTrackerTest.AddCurrentEnemiesToTotal();
        CollectableTrackerTest.AddCurrentDestroyedEnemiesToTotal();
    }

    private void Update()
    {
        if (SceneTracker.SceneChecked)
        {

        }

        _currentEnemyCount = CountTaggedObjectsInScene(_enemyTag);

        CalculateEnemiesDestroyed();

        CollectableTrackerTest.CurrentLevelEnemiesDestroyed = _currentEnemiesDestroyed;

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
        if (_initialEnemyCount != _currentEnemyCount)
        {
            _currentEnemiesDestroyed = _initialEnemyCount - _currentEnemyCount;
        }
    }
}
