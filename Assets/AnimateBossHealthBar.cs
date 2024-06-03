using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBossHealthBar : MonoBehaviour
{
    public Image HealthMask;

    private void Awake()
    {
        HealthMask.fillAmount = 0;
    }

    private void Start()
    {
        StartCoroutine(IncreaseHealthOverTime());
    }

    private IEnumerator IncreaseHealthOverTime()
    {
        yield return new WaitForSeconds(1);

        while (HealthMask.fillAmount < 1)
        {
            // slowly increase the fill amount of the health mask
            HealthMask.fillAmount += Time.deltaTime / 3;
            yield return null; // wait for the next frame before continuing
        }
    }
}
