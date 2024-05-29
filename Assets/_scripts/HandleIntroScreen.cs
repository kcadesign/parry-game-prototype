using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HandleIntroScreen : MonoBehaviour
{
    private void OnEnable()
    {
        HoldButtonToSkip.OnButtonHeldToExecution += HoldButtonToSkip_OnButtonHeldToExecution;
    }

    private void OnDisable()
    {
        HoldButtonToSkip.OnButtonHeldToExecution -= HoldButtonToSkip_OnButtonHeldToExecution;
    }

    private void HoldButtonToSkip_OnButtonHeldToExecution()
    {
        SceneTransitionManager.TransitionManagerInstance.LoadScene("World1-Level1", "WipePinkTransition");
    }
}
