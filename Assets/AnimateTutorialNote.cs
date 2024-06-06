using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTutorialNote : MonoBehaviour
{
    public GameObject SpeechBubble;

    private void OnEnable()
    {
        SpeechBubble.SetActive(false);
        SpeechBubble.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpeechBubble.SetActive(true);
            LeanTween.scale(SpeechBubble, Vector3.one, 0.5f).setEaseOutBack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LeanTween.scale(SpeechBubble, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(() => SpeechBubble.SetActive(false));
        }
    }
}
