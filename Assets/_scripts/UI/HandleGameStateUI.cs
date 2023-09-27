using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameStateUI : MonoBehaviour
{
    public delegate void GameRestart(Vector3 respawnPosition);
    public static event GameRestart OnGameRestart;

    public delegate void GameStateChange(GameObject firstSelectedButton);
    public static event GameStateChange OnGameStateChange;

    [Header("Game Over References")]
    public GameObject GameOverUI;
    public GameObject GameOverFirstSelectedButton;

    [Header("Level Finish References")]
    public GameObject LevelFinishUI;
    public GameObject LevelFinishFirstSelectedButton;

    [Header("Pause References")]
    public GameObject PauseGameUI;
    public GameObject PauseFirstSelectedButton;

    private Vector3 _respawnPoint;

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        HandleLevelProgression.OnSendCurrentCheckpoint += HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPlayerPause += GameStateManager_OnPlayerPause;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        HandleLevelProgression.OnSendCurrentCheckpoint -= HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPlayerPause -= GameStateManager_OnPlayerPause;
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth, bool playerAlive)
    {
        //Debug.Log($"Player is alive: {playerAlive}");

        if(!playerAlive)
        {
            GameOverUI.SetActive(true);
            OnGameStateChange?.Invoke(GameOverFirstSelectedButton);
        }
        else
        {
            GameOverUI.SetActive(false);
        }
    }    

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            LevelFinishUI.SetActive(true);
            OnGameStateChange?.Invoke(LevelFinishFirstSelectedButton);

        }
        else
        {
            LevelFinishUI.SetActive(false);
        }
    }

    private void HandleLevelProgression_OnSendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator)
    {
        _respawnPoint = currentCheckpoint;
    }
    
    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        if (playerPaused)
        {
            PauseGameUI.SetActive(true);
            OnGameStateChange?.Invoke(PauseFirstSelectedButton);

        }
        else
        {
            PauseGameUI.SetActive(false);
        }
    }
    
    public void RestartAtLatestCheckpoint()
    {
        OnGameRestart?.Invoke(_respawnPoint);
        GameOverUI.SetActive(false);
    }

    public void RestartLevel()
    {
        int currentSceneName = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneName);
    }    

}
