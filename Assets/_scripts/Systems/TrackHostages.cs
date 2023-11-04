using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrackHostages : MonoBehaviour
{
    public CollectableTracker CollectableTracker;

    private Scene _currentScene;

    private bool _levelFinished = false;
    private bool _isFollowing = false;

    private void Awake()
    {
        CollectableTracker.InitializeCollectiblesDictionary();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_onSceneLoaded;

        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow += FollowOnTriggerEnter_OnFollow;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_onSceneLoaded;

        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow -= FollowOnTriggerEnter_OnFollow;
    }

    private void SceneManager_onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentScene = scene;
        if (CollectableTracker.CollectiblesDictionary[scene.name])
        {
            // Hostage saved actions here
            Debug.Log($"{scene.name} hostage rescued: {CollectableTracker.CollectiblesDictionary[scene.name]}");
        }
        else if (!CollectableTracker.CollectiblesDictionary[scene.name])
        {
            // Hostage not saved actions here
            Debug.Log($"{scene.name} hostage rescued: {CollectableTracker.CollectiblesDictionary[scene.name]}");
        }
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        Debug.Log("Level finished");
        _levelFinished = levelFinished;

        CheckHostageRescue();
    }

    private void FollowOnTriggerEnter_OnFollow(bool isFollowing)
    {
        _isFollowing = isFollowing;
    }

    private void CheckHostageRescue()
    {
        if (_levelFinished && _isFollowing)
        {
            // Increment number of hostages saved and set hostage saved as true for current level
            CollectableTracker.IncrementHostagesSaved();
            CollectableTracker.CollectiblesDictionary[_currentScene.name] = true;
            Debug.Log($"{_currentScene.name} hostage saved: {CollectableTracker.CollectiblesDictionary[_currentScene.name]}");
        }
    }
}
