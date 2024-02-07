using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameStateUI : MonoBehaviour
{
    public delegate void GameRestart(Vector3 respawnPosition);
    public static event GameRestart OnGameRestart;

    public static event Action OnStartGame;
    public static event Action OnResetGameProgress;
    public static event Action OnExitGame;

    public delegate void GameUIActivate(GameObject firstSelectedButton);
    public static event GameUIActivate OnGameUIActivate;

    [Header("Game Over References")]
    public GameObject GameOverUI;
    public GameObject GameOverFirstSelectedButton;

    [Header("Level Finish References")]
    public GameObject LevelFinishUI;
    public GameObject LevelFinishFirstSelectedButton;

    [Header("Pause References")]
    public GameObject PauseGameUI;
    public GameObject PauseFirstSelectedButton;

    [Header("Start Game References")]
    public GameObject StartGameUI;
    public GameObject StartFirstSelectedButton;

    private Vector3 _respawnPoint;

    private void Awake()
    {
        if(StartGameUI != null)
        {
            OnGameUIActivate?.Invoke(StartFirstSelectedButton);
        }

    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandleBossDeath.OnBossDeathAnimEnd += HandleBossDeath_OnBossDeathAnimEnd;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        HandleLevelProgression.OnSendCurrentCheckpoint += HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPlayerPause += GameStateManager_OnPlayerPause;
    }

    private void HandleBossDeath_OnBossDeathAnimEnd()
    {
        Debug.Log("Boss death anim finished, show game end menu");
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleBossDeath.OnBossDeathAnimEnd -= HandleBossDeath_OnBossDeathAnimEnd;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        HandleLevelProgression.OnSendCurrentCheckpoint -= HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPlayerPause -= GameStateManager_OnPlayerPause;
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth, bool playerAlive)
    {
        //Debug.Log($"Player is alive: {playerAlive}");
        if(GameOverUI != null)
        {
            if (!playerAlive)
            {
                GameOverUI.SetActive(true);
                OnGameUIActivate?.Invoke(GameOverFirstSelectedButton);
            }
            else
            {
                GameOverUI.SetActive(false);
            }
        }
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if(LevelFinishUI != null)
        {
            if (levelFinished)
            {
                LevelFinishUI.SetActive(true);
                OnGameUIActivate?.Invoke(LevelFinishFirstSelectedButton);
            }
            else
            {
                LevelFinishUI.SetActive(false);
            }
        }
    }

    private void HandleLevelProgression_OnSendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator)
    {
        _respawnPoint = currentCheckpoint;
    }
    
    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        if(PauseGameUI != null)
        {
            if (playerPaused)
            {
                PauseGameUI.SetActive(true);
                OnGameUIActivate?.Invoke(PauseFirstSelectedButton);
            }
            else
            {
                PauseGameUI.SetActive(false);
            }
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

    public void NextLevel()
    {
        int currentSceneName = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneName +1);
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void ResetGameProgress()
    {
        OnResetGameProgress?.Invoke();
    }

    public void ExitGame()
    {
        OnExitGame?.Invoke();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
