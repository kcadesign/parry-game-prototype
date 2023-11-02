using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrackHostages : MonoBehaviour
{
    public CollectableTracker CollectableTracker;

    private bool _levelFinished = false;
    private bool _isFollowing = false;

    private void OnEnable()
    {
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow += FollowOnTriggerEnter_OnFollow;
    }


    private void OnDisable()
    {
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow -= FollowOnTriggerEnter_OnFollow;

    }
  
    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        _levelFinished = levelFinished;

        if (CollectableTracker.NewScene)
        {
            CheckHostageRescue();
        }

    }

    private void FollowOnTriggerEnter_OnFollow(bool isFollowing)
    {
        _isFollowing = isFollowing;
    }

    private void CheckHostageRescue()
    {
        if(_levelFinished && _isFollowing)
        {
            CollectableTracker.IncrementHostagesSaved();
        }
    }
}
