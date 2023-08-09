using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SpawnEnemy : ScriptableObject
{
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateEnemy()
    {
        Instantiate(EnemyPrefab, new Vector3(5.41f, -2.693f, 0f), new Quaternion(0,0,0,0));
    }
}
