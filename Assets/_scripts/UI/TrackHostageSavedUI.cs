using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackHostageSavedUI : MonoBehaviour
{
    public Image HostageImage;

    private void Awake()
    {
        HostageImage.enabled = false;
    }

    private void OnEnable()
    {
        FollowOnTriggerEnter.OnFollow += FollowOnTriggerEnter_OnFollow;
    }

    private void OnDisable()
    {
        FollowOnTriggerEnter.OnFollow -= FollowOnTriggerEnter_OnFollow;

    }

    private void FollowOnTriggerEnter_OnFollow(bool isFollowing)
    {
        if (isFollowing)
        {
            HostageImage.enabled = true;
        }
        else
        {
            HostageImage.enabled = false;
        }

    }
}
