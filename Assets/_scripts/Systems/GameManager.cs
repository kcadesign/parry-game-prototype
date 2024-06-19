using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SaveData SaveData;

    public static GameManager GameManagerInstance;

    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SaveData = new SaveData();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        MainMenu.OnStartButtonPressed += MainMenuManager_OnStartButtonPressed;
        MainMenu.OnResetProgressButtonPressed += MainMenuManager_OnResetProgressButtonPressed;
        MainMenu.OnExitGameButtonPressed += MainMenuManager_OnExitGameButtonPressed;

        HandleGameStateUI.OnRestartButtonPressed += HandleGameStateUI_OnRestartButtonPressed;
        HandleGameStateUI.OnMenuButtonPressed += HandleGameStateUI_OnMenuButtonPressed;
        HandleGameStateUI.OnExitGameButtonPressed += HandleGameStateUI_OnExitGameButtonPressed;

        //HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnPlayerParryFinish;
        WarpTransition.OnWarpTransitionEnd += WarpTransition_OnWarpTransitionEnd;

        HandleBossDeath.OnBossDeathAnimEnd += HandleBossDeath_OnBossDeathAnimEnd;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        MainMenu.OnStartButtonPressed -= MainMenuManager_OnStartButtonPressed;
        MainMenu.OnResetProgressButtonPressed -= MainMenuManager_OnResetProgressButtonPressed;
        MainMenu.OnExitGameButtonPressed -= MainMenuManager_OnExitGameButtonPressed;

        HandleGameStateUI.OnRestartButtonPressed -= HandleGameStateUI_OnRestartButtonPressed;
        HandleGameStateUI.OnMenuButtonPressed -= HandleGameStateUI_OnMenuButtonPressed;
        HandleGameStateUI.OnExitGameButtonPressed -= HandleGameStateUI_OnExitGameButtonPressed;

        //HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnPlayerParryFinish;
        WarpTransition.OnWarpTransitionEnd -= WarpTransition_OnWarpTransitionEnd;

        HandleBossDeath.OnBossDeathAnimEnd -= HandleBossDeath_OnBossDeathAnimEnd;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
    }

    private void MainMenuManager_OnStartButtonPressed()
    {
        SaveData.LoadGameProgress();
        LoadSavedOrDefaultScene();
    }
    private void MainMenuManager_OnResetProgressButtonPressed()
    {
        SaveData.ResetGameProgress();
        // Create reset progress visual effect for here
    }

    private void MainMenuManager_OnExitGameButtonPressed()
    {
        QuitGame();
    }

    private void HandleGameStateUI_OnRestartButtonPressed()
    {
        SceneTransitionManager.TransitionManagerInstance.LoadScene(SceneManager.GetActiveScene().name, "CrossFadeWhiteTransition");
    }

    private void HandleGameStateUI_OnMenuButtonPressed()
    {
        SaveData.SaveGameProgress(SceneManager.GetActiveScene().name);
        SceneTransitionManager.TransitionManagerInstance.LoadScene("Menu-Start", "CrossFadeWhiteTransition");
    }

    private void HandleGameStateUI_OnExitGameButtonPressed()
    {
        SaveData.SaveGameProgress(SceneManager.GetActiveScene().name);
        QuitGame();
    }

    private void WarpTransition_OnWarpTransitionEnd()
    {
        SaveData.SaveGameProgress(SceneManager.GetActiveScene().name);
        Debug.Log("Save level: " + SceneManager.GetActiveScene().name);
    }

    private void HandleBossDeath_OnBossDeathAnimEnd()
    {
        SceneTransitionManager.TransitionManagerInstance.LoadScene("Story-OutroScreen", "CrossFadeWhiteTransition");
    }

    public void LoadSavedOrDefaultScene()
    {
        if (string.IsNullOrEmpty(SaveData.SavedScene))
        {
            Debug.Log($"Loading default scene");
            SceneTransitionManager.TransitionManagerInstance.LoadScene("Story-IntroScreen", "WipePinkTransition");
        }
        else
        {
            Debug.Log($"Loading saved scene: {SaveData.SavedScene}");
            SceneTransitionManager.TransitionManagerInstance.LoadScene(SaveData.SavedScene, "WipePinkTransition");
        }
    }

    private static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
