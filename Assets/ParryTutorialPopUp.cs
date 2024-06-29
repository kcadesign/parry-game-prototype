using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTutorialPopUp : MonoBehaviour
{
    public GameObject TutorialPopUp;
    public GameObject Enemy;

    private void Awake()
    {
        TutorialPopUp.SetActive(false);
    }

    private void OnEnable()
    {
        AOEEnemyController.OnAttackWarning += AOEEnemyController_OnAttackWarning;
    }

    private void OnDisable()
    {
        AOEEnemyController.OnAttackWarning -= AOEEnemyController_OnAttackWarning;
    }

    private void AOEEnemyController_OnAttackWarning()
    {
        StartCoroutine(ActiveTimeOut());
    }

    private IEnumerator ActiveTimeOut()
    {
        if (!TutorialPopUp.activeSelf)
        {
            TutorialPopUp.SetActive(true);
        }
        else if (TutorialPopUp.activeSelf)
        {
            yield return new WaitForSeconds(0.5f);
            TutorialPopUp.SetActive(false);
        }
    }
}
