using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CollectableTracker CollectableTracker;
    public SaveData SaveData;

    private int _nextScene;

    private void Awake()
    {
        _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnEnable()
    {        
        HandleGameStateUI.OnStartGame += HandleMainMenuUI_OnStartGame;
        HandleGameStateUI.OnResetGameProgress += HandleMainMenuUI_OnResetGameProgress;
        HandleGameStateUI.OnExitGame += HandleGameStateUI_OnExitGame;
    }


    private void OnDisable()
    {
        HandleGameStateUI.OnStartGame -= HandleMainMenuUI_OnStartGame;
        HandleGameStateUI.OnResetGameProgress -= HandleMainMenuUI_OnResetGameProgress;
        HandleGameStateUI.OnExitGame -= HandleGameStateUI_OnExitGame;
    }

    private void HandleMainMenuUI_OnStartGame()
    {
        Debug.Log("Starting game...");
        SaveData.LoadGameProgress();
        LoadSavedSceneOrDefault();
    }
    private void HandleMainMenuUI_OnResetGameProgress()
    {
        CollectableTracker.ClearAllFields();
        SaveData.ResetGameProgress();
    }

    private void HandleGameStateUI_OnExitGame()
    {
        SaveData.SaveGameProgress(CollectableTracker.CurrentSceneName);
        CollectableTracker.ClearAllFields();
    }

    public void LoadSavedSceneOrDefault()
    {
        if (string.IsNullOrEmpty(CollectableTracker.CurrentSceneName))
        {
            Debug.Log($"Loading default scene: {_nextScene}");
            SceneManager.LoadScene(_nextScene);
        }
        else
        {
            Debug.Log($"Loading saved scene: {CollectableTracker.CurrentSceneName}");
            SceneManager.LoadScene(CollectableTracker.CurrentSceneName);
        }
    }

}
