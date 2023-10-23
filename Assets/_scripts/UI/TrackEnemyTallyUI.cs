using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackEnemyTallyUI : MonoBehaviour
{
    private GameObject[] _taggedObjectArray;
    private int _initialNumberOfTaggedObjects;
    private int _currentNumberOfTaggedObjects;
    private int _numberOfEnemiesDestroyed;

    public TextMeshProUGUI EnemyCounterText;

    private void OnEnable()
    {
        TrackEnemies.OnGetEnemyCount += TrackKills_OnGetEnemyCount;
    }

    private void OnDisable()
    {
        TrackEnemies.OnGetEnemyCount -= TrackKills_OnGetEnemyCount;
    }

    private void TrackKills_OnGetEnemyCount(GameObject[] obj)
    {
        _taggedObjectArray = obj;

    }

    void Start()
    {
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
        _currentNumberOfTaggedObjects = _taggedObjectArray.Length;
        _numberOfEnemiesDestroyed = _initialNumberOfTaggedObjects - _currentNumberOfTaggedObjects;

        EnemyCounterText.text = $"{_numberOfEnemiesDestroyed} / {_initialNumberOfTaggedObjects}";
    }

}
