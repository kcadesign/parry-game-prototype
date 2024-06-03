using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameStateUI : MonoBehaviour
{
    public static event Action OnRestartButtonPressed;
    public static event Action OnMenuButtonPressed;
    public static event Action OnExitGameButtonPressed;

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
        //HandleLevelProgression.OnSendCurrentCheckpoint += HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPauseButtonPressed;
    }

    private void OnDisable()
    {
        HandlePlayerDeath.OnPlayerDeathAnimEnd -= HandlePlayerDeath_OnPlayerDeathAnimEnd;
        //HandleLevelProgression.OnSendCurrentCheckpoint -= HandleLevelProgression_OnSendCurrentCheckpoint;
        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPauseButtonPressed;
    }

    private void Start()
    {
        PauseGameUI.transform.position = ScreenBelow.transform.position;
        PauseGameUI.SetActive(false);
        GameOverUI.transform.position = ScreenBelow.transform.position;
        GameOverUI.SetActive(false);
    }

    private void HandlePlayerDeath_OnPlayerDeathAnimEnd()
    {
        StartCoroutine(GameUIIn(GameOverUI));
        OnGameUIActivate?.Invoke(GameOverFirstSelectedButton);
    }

    /*    private void HandleLevelProgression_OnSendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator)
        {
            _respawnPoint = currentCheckpoint;
        }
    */
    private void GameStateManager_OnPauseButtonPressed(bool playerPaused)
    {
        if (playerPaused)
        {
            StartCoroutine(GameUIIn(PauseGameUI));
            OnGameUIActivate?.Invoke(PauseFirstSelectedButton);
        }
        else
        {
            StartCoroutine(GameUIOut(PauseGameUI));
        }
    }

    private IEnumerator GameUIIn(GameObject inGameUI)
    {
        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
            LeanTween.moveY(inGameUI, ScreenMiddle.transform.position.y, 1f)
                    .setEase(LeanTweenType.easeOutExpo)
                    .setIgnoreTimeScale(true);
        }
        yield return new WaitWhile(() => LeanTween.isTweening(inGameUI));
    }

    private IEnumerator GameUIOut(GameObject inGameUI)
    {
        if (inGameUI != null)
        {
            LeanTween.moveY(inGameUI, ScreenBelow.transform.position.y, 1f)
                     .setEase(LeanTweenType.easeInOutExpo)
                     .setIgnoreTimeScale(true);

            // Wait until the tweening is done
            yield return new WaitWhile(() => LeanTween.isTweening(inGameUI));

            // Deactivate the UI element after the animation is done
            inGameUI.SetActive(false);
        }
    }

    /*    public void RestartAtLatestCheckpoint()
        {
            OnRestartButtonPressed?.Invoke(_respawnPoint);
            GameOverUI.SetActive(false);
        }
    */
    public void RestartLevelButtonPressed()
    {
        OnRestartButtonPressed?.Invoke();
    }

    public void MainMenuButtonPressed()
    {
        OnMenuButtonPressed?.Invoke();
    }

    public void ExitGameButtonPressed()
    {
        OnExitGameButtonPressed?.Invoke();
    }

}
