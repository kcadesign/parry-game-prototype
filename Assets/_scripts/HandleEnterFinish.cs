using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnterFinish : MonoBehaviour
{
    public delegate void FinishLevel(bool levelFinished);
    public static event FinishLevel OnLevelFinish;

    private bool _levelFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _levelFinished = true;
        }
        else
        {
            _levelFinished = false;
        }
        OnLevelFinish?.Invoke(_levelFinished);
    }
}