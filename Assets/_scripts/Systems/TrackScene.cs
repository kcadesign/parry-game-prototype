using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackScene : MonoBehaviour
{
    public static event Action OnSceneChecked;

    public CollectableTracker CollectableTracker;

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
        CollectableTracker.CheckIfSceneChanged(scene.name);
        CollectableTracker.StoreCurrentSceneName(scene.name);
        OnSceneChecked?.Invoke();
        CollectableTracker.AddSceneNameToList(scene.name);
    }

}
