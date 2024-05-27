using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameStateUI : MonoBehaviour
{
    public delegate void GameRestart(Vector3 respawnPosition);
    public static event GameRestart OnGameRestart;

    public static event Action OnGoToMainMenu;
    public static event Action OnExitGame;

    public delegate void GameUIActivate(GameObject firstSelectedButton);
    public static event GameUIActivate OnGameUIActivate;

    [Header("UI Position References")]
    public GameObject ScreenAbove;
    public GameObject ScreenMiddle;
    public GameObject ScreenBelow;

    [Header("Game Over References")]
    public GameObject GameOverUI;
    public GameObject GameOverFirstSelectedButton;

    [Header("Pause References")]
    public GameObject PauseGameUI;
    public GameObject PauseFirstSelectedButton;

    private Vector3 _respawnPoint;

    private void OnEnable()
    {
        HandlePlayerDeath.OnPlayerDeathAnimEnd += HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleLevelProgression.OnSendCurrentCheckpoint += HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPauseButtonPressed;
    }

    private void OnDisable()
    {
        HandlePlayerDeath.OnPlayerDeathAnimEnd -= HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleLevelProgression.OnSendCurrentCheckpoint -= HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPauseButtonPressed;
    }

    private void Start()
    {
        PauseGameUI.transform.position = ScreenBelow.transform.position;
    }

    private void HandlePlayerDeath_OnPlayerDeathAnimEnd()
    {
        if (GameOverUI != null)
        {
            GameOverUI.SetActive(true);
            OnGameUIActivate?.Invoke(GameOverFirstSelectedButton);
        }
    }

    private void HandleLevelProgression_OnSendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator)
    {
        _respawnPoint = currentCheckpoint;
    }

    private void GameStateManager_OnPauseButtonPressed(bool playerPaused)
    {
        if (playerPaused)
        {
            StartCoroutine(PauseProcedureIn());
        }
        else
        {
            StartCoroutine(PauseProcedureOut());
        }
    }

    private IEnumerator PauseProcedureIn()
    {
        if (PauseGameUI != null)
        {
            PauseGameUI.SetActive(true);
            LeanTween.moveY(PauseGameUI, ScreenMiddle.transform.position.y, 1f)
                    .setEase(LeanTweenType.easeOutExpo)
                    .setIgnoreTimeScale(true);
        }
        yield return new WaitWhile(() => LeanTween.isTweening(PauseGameUI));
        OnGameUIActivate?.Invoke(PauseFirstSelectedButton);
    }

    private IEnumerator PauseProcedureOut()
    {
        if (PauseGameUI != null)
        {
            LeanTween.moveY(PauseGameUI, ScreenBelow.transform.position.y, 1f)
                     .setEase(LeanTweenType.easeInOutExpo)
                     .setIgnoreTimeScale(true);

            // Wait until the tweening is done
            yield return new WaitWhile(() => LeanTween.isTweening(PauseGameUI));

            // Deactivate the UI element after the animation is done
            PauseGameUI.SetActive(false);
        }
    }

    public void RestartAtLatestCheckpoint()
    {
        OnGameRestart?.Invoke(_respawnPoint);
        GameOverUI.SetActive(false);
    }

    public void RestartLevelButtonPressed()
    {
        int currentSceneName = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenuButtonPressed()
    {
        OnGoToMainMenu?.Invoke();
        SceneManager.LoadScene(1);
    }

    public void ExitGameButtonPressed()
    {
        OnExitGame?.Invoke();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
