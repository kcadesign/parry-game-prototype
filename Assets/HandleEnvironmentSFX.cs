using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleEnvironmentSFX : MonoBehaviour
{
    public SoundCollection _environmentSounds;

    private void OnEnable()
    {
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnPlayerParryFinish;
        //HandleEnterFinish.OnWarpFinish += HandleEnterFinish_OnWarpFinish;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnPlayerParryFinish;
        //HandleEnterFinish.OnWarpFinish -= HandleEnterFinish_OnWarpFinish;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void HandleEnterFinish_OnPlayerParryFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            _environmentSounds.PlaySound("LevelEndWarp", transform);
        }
    }

    /*    private void HandleEnterFinish_OnWarpFinish()
        {
            _environmentSounds.PlaySound("Warp", transform);
        }
    */

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currentLevel != 1)
        {
            _environmentSounds.PlaySound("LevelStartWarp", transform);
        }

    }

}
