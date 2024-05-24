using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static event Action OnStartButtonPressed;
    public static event Action OnResetProgressButtonPressed;
    public static event Action OnExitGameButtonPressed;

    public void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
    }

    public void ResetProgressButtonPressed()
    {
        OnResetProgressButtonPressed?.Invoke();
    }

    public void ExitGameButtonPressed()
    {
        OnExitGameButtonPressed?.Invoke();
    }
}
