using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseTrackers : MonoBehaviour
{
    public CollectableTrackerTest CollectableTrackerTest;

    private void Awake()
    {
        CollectableTrackerTest.PopulateAndInitialiseDictionaries();
    }



}
