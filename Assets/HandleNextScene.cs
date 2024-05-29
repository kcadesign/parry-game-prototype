using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNextScene : MonoBehaviour
{
    public string NextSceneName;
    public string TransitionName;

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
        SceneTransitionManager.TransitionManagerInstance.LoadScene(NextSceneName, TransitionName);
    }
}
