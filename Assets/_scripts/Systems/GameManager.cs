using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CollectableTracker CollectableTracker;
    public SaveData SaveData;

    private string _firstLevelName = "TestLevel1";

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
        SaveData.LoadGameProgress();
        LoadSavedSceneOrDefault();
    }
    private void HandleMainMenuUI_OnResetGameProgress()
    {
        SaveData.ResetGameProgress();
    }

    private void HandleGameStateUI_OnExitGame()
    {
        SaveData.SaveGameProgress(CollectableTracker.CurrentSceneName);
        CollectableTracker.ClearAllFields();
    }

    private void LoadSavedSceneOrDefault()
    {
        if (!string.IsNullOrEmpty(CollectableTracker.CurrentSceneName))
        {
            SceneManager.LoadScene(CollectableTracker.CurrentSceneName);
        }
        else
        {
            SceneManager.LoadScene(_firstLevelName);
        }
    }

}
