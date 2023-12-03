using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnterFinish : MonoBehaviour
{
    public delegate void FinishLevel(bool levelFinished);
    public static event FinishLevel OnLevelFinish;

    private bool _parryActive;
    private bool _levelFinished;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _parryActive)
        {
            _levelFinished = true;
            OnLevelFinish?.Invoke(_levelFinished);
        }
        else
        {
            _levelFinished = false;
        }

    }
}