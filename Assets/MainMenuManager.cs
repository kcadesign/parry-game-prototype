using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static Action OnStartButtonPressed;


    public void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
        SceneTransitionManager.instance.LoadScene("Story-IntroScreen", "WipePink");

    }
}
