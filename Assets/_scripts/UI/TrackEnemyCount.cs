using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackEnemyCount : MonoBehaviour
{
    private string _enemyTag = "Enemy";
    private GameObject[] _taggedObjectArray;
    private int _initialNumberOfTaggedObjects;
    private int _currentNumberOfTaggedObjects;
    private int _numberOfEnemiesDestroyed;

    public TextMeshProUGUI EnemyCounterText;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        GetTaggedObjectsInScene(_enemyTag);

        _initialNumberOfTaggedObjects = _taggedObjectArray.Length;

        /*
        // Output the count.
        Debug.Log($"{_numberOfTaggedObjects} objects with {_enemyTag} tag in scene");

        foreach (GameObject enemyObject in _taggedObjectArray)
        {
            Debug.Log("Found enemy: " + enemyObject.name);
        }*/
    }

    private void Update()
    {
        GetTaggedObjectsInScene(_enemyTag);
        _currentNumberOfTaggedObjects = _taggedObjectArray.Length;
        _numberOfEnemiesDestroyed = _initialNumberOfTaggedObjects - _currentNumberOfTaggedObjects;

        EnemyCounterText.text = $"{_numberOfEnemiesDestroyed} / {_initialNumberOfTaggedObjects}";
    }

    private void GetTaggedObjectsInScene(string tag)
    {
        _taggedObjectArray = GameObject.FindGameObjectsWithTag(tag);
    }

}
