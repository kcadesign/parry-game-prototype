using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CollectableTracker CollectableTracker;
    public SaveData SaveData;

    private void OnEnable()
    {        
        MainMenu.OnStartButtonPressed += MainMenuManager_OnStartButtonPressed;
        MainMenu.OnResetProgressButtonPressed += MainMenuManager_OnResetProgressButtonPressed;
        MainMenu.OnExitGameButtonPressed += MainMenuManager_OnExitGameButtonPressed;
    }

    private void OnDisable()
    {
        MainMenu.OnStartButtonPressed -= MainMenuManager_OnStartButtonPressed;
        MainMenu.OnResetProgressButtonPressed -= MainMenuManager_OnResetProgressButtonPressed;
        MainMenu.OnExitGameButtonPressed -= MainMenuManager_OnExitGameButtonPressed;
    }

    private void MainMenuManager_OnStartButtonPressed()
    {
        Debug.Log("Starting session...");
        SaveData.LoadGameProgress();
        LoadSavedSceneOrDefault();
    }
    private void MainMenuManager_OnResetProgressButtonPressed()
    {
        CollectableTracker.ClearAllFields();
        SaveData.ResetGameProgress();

        // Create reset progress visual effect for here
    }

    private void MainMenuManager_OnExitGameButtonPressed()
    {
        SaveData.SaveGameProgress(CollectableTracker.SavedSceneName);
        CollectableTracker.ClearAllFields();

        // Create exit game transition for here

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }

    public void LoadSavedSceneOrDefault()
    {
        if (string.IsNullOrEmpty(CollectableTracker.SavedSceneName))
        {
            Debug.Log($"Loading default scene");
            SceneTransitionManager.TransitionManagerInstance.LoadScene("Story-IntroScreen", "WipePinkTransition");
        }
        else
        {
            Debug.Log($"Loading saved scene: {CollectableTracker.SavedSceneName}");
            //SceneManager.LoadScene(CollectableTracker.CurrentSceneName);
            SceneTransitionManager.TransitionManagerInstance.LoadScene(CollectableTracker.SavedSceneName, "WipePink");
        }
    }

}
