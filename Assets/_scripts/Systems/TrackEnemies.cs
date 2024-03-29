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

    private void OnEnable()
    {
        TrackScene.OnSceneChecked += TrackScene_OnSceneChecked;
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnLevelFinish;

    }

    private void OnDisable()
    {
        TrackScene.OnSceneChecked -= TrackScene_OnSceneChecked;
        HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnLevelFinish;

    }

    private void TrackScene_OnSceneChecked()
    {
        _initialEnemyCount = CountTaggedObjectsInScene(_enemyTag);
        CollectableTracker.CurrentLevelEnemyCount = _initialEnemyCount;

        CollectableTracker.AddCurrentEnemiesToTotal();


    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        Debug.Log($"Level finished: {levelFinished}");
        if (levelFinished)
        {
            //CollectableTracker.UpdateEnemiesDestroyedDictionary(_currentEnemiesDestroyed);
            CollectableTracker.AddCurrentDestroyedEnemiesToTotal(_currentEnemiesDestroyed);
        }
    }

    private void Update()
    {
        _currentEnemyCount = CountTaggedObjectsInScene(_enemyTag);

        CalculateEnemiesDestroyed();

        //CollectableTrackerTest.CurrentLevelEnemiesDestroyed = _currentEnemiesDestroyed;

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
