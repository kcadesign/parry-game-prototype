using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CollectableTracker CollectableTracker;
    public SaveData SaveData;

    private string _firstLevelName = "Level1";

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

    private void LoadSavedSceneOrDefault()
    {
        if (string.IsNullOrEmpty(CollectableTracker.CurrentSceneName))
        {
            Debug.Log($"Loading default scene: {_firstLevelName}");
            SceneManager.LoadScene(_firstLevelName);
        }
        else
        {
            Debug.Log($"Loading saved scene: {CollectableTracker.CurrentSceneName}");
            SceneManager.LoadScene(CollectableTracker.CurrentSceneName);
        }
    }

}
