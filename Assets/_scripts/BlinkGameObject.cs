using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkGameObject : MonoBehaviour
{
    public GameObject UIItem;

    [SerializeField] private float _blinkInterval = 1.0f;

    void Start()
    {
        StartCoroutine(ToggleObjectActive(_blinkInterval));
    }

    private IEnumerator ToggleObjectActive(float interval)
    {
        while (true)
        {
            if (UIItem != null)
            {
                UIItem.SetActive(!UIItem.activeSelf);
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
