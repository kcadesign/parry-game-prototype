using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnemies : MonoBehaviour
{
    public static event Action<GameObject[]> OnGetEnemyCount;

    public CollectableTracker CollectableTracker;

    private string _enemyTag = "Enemy";
    private GameObject[] _taggedObjectArray;

    void Start()
    {
        GetTaggedObjectsInScene(_enemyTag);
        CollectableTracker.TotalEnemies += _taggedObjectArray.Length;
    }

    private void OnDisable()
    {
        CollectableTracker.ResetTracker();

    }

    private void Update()
    {
        GetTaggedObjectsInScene(_enemyTag);
    }

    private void GetTaggedObjectsInScene(string tag)
    {
        _taggedObjectArray = GameObject.FindGameObjectsWithTag(tag);
        OnGetEnemyCount?.Invoke(_taggedObjectArray);
    }

}
