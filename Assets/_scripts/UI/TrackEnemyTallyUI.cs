using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackEnemyTallyUI : MonoBehaviour
{
    private int _totalEnemies;
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

    private void TrackKills_OnGetEnemyCount(int enemiesDestroyed, int totalEnemies)
    {
        _numberOfEnemiesDestroyed = enemiesDestroyed;
        _totalEnemies = totalEnemies;
    }

    void Start()
    {
        // Output the count.
        //Debug.Log($"{_initialNumberOfTaggedObjects} objects with enemy tag in scene");
        /*
        foreach (GameObject enemyObject in _taggedObjectArray)
        {
            Debug.Log("Found enemy: " + enemyObject.name);
        }*/
    }

    private void Update()
    {

        EnemyCounterText.text = $"{_numberOfEnemiesDestroyed} / {_totalEnemies}";
    }

}
