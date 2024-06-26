using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static event Action OnStartButtonPressed;
    public static event Action OnResetProgressButtonPressed;
    public static event Action OnExitGameButtonPressed;

    [Header("UI Position References")]
    public GameObject ScreenMiddle;
    public GameObject ScreenBelow;

    [Header("Reset Confirm References")]
    public GameObject ResetConfirmUI;
    public GameObject ResetButtonContainer;
    public GameObject ResetConfirmFirstSelectedButton;
    public TextMeshProUGUI ResetConfirmTMP;
    public TextMeshProUGUI ResetConfirmTMPShadow;

    [Header("Main Menu References")]
    public GameObject MainMenuButtonContainer;
    public GameObject MainMenuFirstSelectedButton;

    private void Start()
    {
        ResetConfirmUI.transform.position = ScreenBelow.transform.position;
        ResetConfirmUI.SetActive(false);
    }

    public void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
    }

    public void ResetProgressButtonPressed()
    {
        StartCoroutine(AnimateResetUIIn(ResetConfirmUI));
    }

    public void ExitGameButtonPressed()
    {
        OnExitGameButtonPressed?.Invoke();
    }

    public void ConfirmResetButtonPressed()
    {
        OnResetProgressButtonPressed?.Invoke();
        StartCoroutine(DataResetAnim(ResetConfirmUI));
        //MainMenuButtonContainer.SetActive(true);
        //StartCoroutine(SetSelectedAfterOneFrame());
    }

    public void CancelResetButtonPressed()
    {
        StartCoroutine(AnimateResetUIOut(ResetConfirmUI));
        //StartCoroutine(SetSelectedAfterOneFrame());
    }

    private IEnumerator AnimateResetUIIn(GameObject gameUI)
    {
        yield return new WaitForSeconds(0.5f);

        if (gameUI != null)
        {
            gameUI.SetActive(true);
            LeanTween.moveY(gameUI, ScreenMiddle.transform.position.y, 1f)
                    .setEase(LeanTweenType.easeOutExpo)
                    .setIgnoreTimeScale(true);
        }

        yield return new WaitWhile(() => LeanTween.isTweening(gameUI));
        MainMenuButtonContainer.SetActive(false);

    }

    private IEnumerator AnimateResetUIOut(GameObject gameUI)
    {
        yield return new WaitForSeconds(0.5f);

        if (gameUI != null)
        {
            LeanTween.moveY(gameUI, ScreenBelow.transform.position.y, 1f)
                     .setEase(LeanTweenType.easeInOutExpo)
                     .setIgnoreTimeScale(true);

            // Wait until the tweening is done
            yield return new WaitWhile(() => LeanTween.isTweening(gameUI));
            Debug.Log("Reset progress UI tweening done");
            MainMenuButtonContainer.SetActive(true);

            // Deactivate the UI element after the animation is done
            gameUI.SetActive(false);
        }
    }

    private IEnumerator DataResetAnim(GameObject gameUI)
    {
        yield return new WaitForSeconds(0.5f);

        ResetConfirmTMP.text = "Data successfully reset";
        ResetConfirmTMPShadow.text = "Data successfully reset";
        ResetButtonContainer.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        LeanTween.moveY(gameUI, ScreenBelow.transform.position.y, 1f)
                 .setEase(LeanTweenType.easeInOutExpo)
                 .setIgnoreTimeScale(true);

        // Wait until the tweening is done
        yield return new WaitWhile(() => LeanTween.isTweening(gameUI));

        MainMenuButtonContainer.SetActive(true);

        ResetConfirmTMP.text = "Are you sure you would like to reset all progress?";
        ResetConfirmTMPShadow.text = "Are you sure you would like to reset all progress?";
        ResetButtonContainer.SetActive(true);

        // Deactivate the UI element after the animation is done
        gameUI.SetActive(false);
    }

    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(MainMenuFirstSelectedButton);
        MainMenuFirstSelectedButton.GetComponent<ButtonSelectionHandler>().ButtonSelector.SetActive(true);
    }

}
