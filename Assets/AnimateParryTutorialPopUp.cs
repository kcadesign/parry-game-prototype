using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateParryTutorialPopUp : MonoBehaviour
{
    public GameObject TutorialAssets;
    public GameObject Enemy;

    private void Awake()
    {
        TutorialAssets.SetActive(false);
        TutorialAssets.transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnEnable()
    {
        AOEEnemyController.OnAttackWarning += AOEEnemyController_OnAttackWarning;
    }

    private void OnDisable()
    {
        AOEEnemyController.OnAttackWarning -= AOEEnemyController_OnAttackWarning;

        StopCoroutine(AnimatePopUp());
    }

    private void AOEEnemyController_OnAttackWarning()
    {
        StartCoroutine(AnimatePopUp());
    }

    private IEnumerator AnimatePopUp()
    {
        TutorialAssets.SetActive(true);
        LeanTween.scale(TutorialAssets, new Vector3(1f, 1f, 1f), 0.25f).setEaseInExpo();
        yield return new WaitWhile(() => LeanTween.isTweening(TutorialAssets));
        LeanTween.scale(TutorialAssets, new Vector3(0.8f, 0.8f, 0.8f), 0.25f);
        yield return new WaitWhile(() => LeanTween.isTweening(TutorialAssets));
        LeanTween.scale(TutorialAssets, new Vector3(1f, 1f, 1f), 0.25f);
        yield return new WaitWhile(() => LeanTween.isTweening(TutorialAssets));
        LeanTween.scale(TutorialAssets, new Vector3(0, 0f, 0f), 0.25f).setEaseInExpo();
        yield return new WaitWhile(() => LeanTween.isTweening(TutorialAssets));
        TutorialAssets.SetActive(false);
    }
}
