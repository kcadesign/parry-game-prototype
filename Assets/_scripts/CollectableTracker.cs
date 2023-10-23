using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectableTracker : ScriptableObject
{
    public int EnemyCount;
    public int TotalEnemies;
    public int Enemies_destroyed;

    public void ResetTracker()
    {
        EnemyCount = 0;
        TotalEnemies =0;
        Enemies_destroyed =0;

    }
}
