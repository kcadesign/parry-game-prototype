using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAppearAnim : MonoBehaviour
{
    public GameObject[] Buttons;

    void Start()
    {
        // move the buttons off screen
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.localPosition = new Vector3(0, Buttons[i].transform.localPosition.y - 1000, 0);
        }

        // move the buttons from off screen to their starting position using lean tween\
        for (int i = 0; i < Buttons.Length; i++)
        {
            LeanTween.moveLocalY(Buttons[i], Buttons[i].transform.localPosition.y + 1000, 1f).setEase(LeanTweenType.easeOutExpo).setDelay(i * 0.25f);
        }
    }
}
