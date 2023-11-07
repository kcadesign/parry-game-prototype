using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackScene : MonoBehaviour
{
    public CollectableTrackerTest CollectableTrackerTest;

    public bool SceneChecked = false;

    private void Awake()
    {
        SceneChecked = false;
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
        if (!SceneChecked)
        {
            CollectableTrackerTest.CheckIfSceneChanged(scene.name);
            CollectableTrackerTest.StoreScene(scene.name);
            SceneChecked = true;
        }
    }

}
