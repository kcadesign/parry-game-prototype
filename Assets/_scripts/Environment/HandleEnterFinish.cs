using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleEnterFinish : MonoBehaviour
{
    public delegate void FinishLevel(bool levelFinished);
    public static event FinishLevel OnPlayerParryFinish;

    private bool _parryActive;
    private bool _levelFinished;

    private Scene _currentScene;
    private string _nextSceneName;

    private void Awake()
    {
        _currentScene = SceneManager.GetActiveScene();
        _nextSceneName = GetNextSceneName();
        //Debug.Log(_nextSceneName);
    }

    private string GetNextSceneName()
    {
        int nextBuildIndex = _currentScene.buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] sceneNames = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        // Find the index of the next scene
        int nextSceneIndex = nextBuildIndex % sceneCount;

        return sceneNames[nextSceneIndex];
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _parryActive)
        {
            _levelFinished = true;
            OnPlayerParryFinish?.Invoke(_levelFinished);
            if (_nextSceneName != null)
            {
                SceneTransitionManager.TransitionManagerInstance.LoadScene(_nextSceneName, "WarpTransition");
            }
            else
            {
                Debug.LogWarning("Next scene not found!");
            }
        }
        else
        {
            _levelFinished = false;
        }
    }
}
