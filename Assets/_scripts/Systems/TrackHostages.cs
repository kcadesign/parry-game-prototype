using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackHostages : MonoBehaviour
{
    public CollectableTrackerTest CollectableTrackerTest;

    private bool _levelFinished = false;
    private bool _isFollowing = false;

    private string _hostageTag = "Hostage";
    private bool _hostagePresent;
    private bool _hostageRescued;

    private void OnEnable()
    {
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow += FollowOnTriggerEnter_OnFollow;
        TrackScene.OnSceneChecked += TrackScene_OnSceneChecked;
    }

    private void OnDisable()
    {
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        FollowOnTriggerEnter.OnFollow -= FollowOnTriggerEnter_OnFollow;
        TrackScene.OnSceneChecked -= TrackScene_OnSceneChecked;
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        Debug.Log($"Level finished: {levelFinished}");
        _levelFinished = levelFinished;

        CheckHostageRescue();
        CollectableTrackerTest.UpdateHostageRescuedDictionary(_hostageRescued);
    }

    private void FollowOnTriggerEnter_OnFollow(bool isFollowing)
    {
        _isFollowing = isFollowing;
    }

    private void TrackScene_OnSceneChecked()
    {
        _hostagePresent = CheckForTagInScene(_hostageTag);
        Debug.Log($"Hostage present in scene: {_hostagePresent}");
        if (_hostagePresent)
        {
            CollectableTrackerTest.AddHostageToTotal();
        }
    }

    private void CheckHostageRescue()
    {
        if (_levelFinished && _isFollowing)
        {
            CollectableTrackerTest.AddSavedHostageToTotal();

            _hostageRescued = true;
        }
        _hostageRescued = false;
    }

    private bool CheckForTagInScene(string tag)
    {
        bool hostagePresent = GameObject.FindGameObjectWithTag(tag);
        return hostagePresent;
    }
}
