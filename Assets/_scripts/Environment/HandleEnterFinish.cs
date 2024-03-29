using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnterFinish : MonoBehaviour
{
    public delegate void FinishLevel(bool levelFinished);
    public static event FinishLevel OnPlayerParryFinish;

    public static Action OnWarpFinish;

    public GameObject WarpObject;
    private Material LevelFinishWarpMaterial;

    private bool _parryActive;
    private bool _levelFinished;

    [SerializeField] private float _shockwaveTime = 3f;
    private int _waveDistanceFromCentre = Shader.PropertyToID("_WaveDistanceFromCentre");
    private int _ShockwaveStrength = Shader.PropertyToID("_ShockwaveStrength");

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        WarpObject.SetActive(false);
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
            OnPlayerParryFinish?.Invoke(_levelFinished);
            StartCoroutine(LevelWarp(0, 5));
        }
        else
        {
            _levelFinished = false;
        }
    }

    private IEnumerator LevelWarp(float startPosition, float endPosition)
    {
        WarpObject.SetActive(true);
        LevelFinishWarpMaterial = WarpObject.GetComponent<SpriteRenderer>().material;

        LevelFinishWarpMaterial.SetFloat(_ShockwaveStrength, startPosition);

        float lerpedAmount = -0.1f;
        float elapsedTime = 0;
        while (elapsedTime < _shockwaveTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            lerpedAmount = Mathf.Lerp(startPosition, endPosition, (elapsedTime / _shockwaveTime));
            LevelFinishWarpMaterial.SetFloat(_ShockwaveStrength, lerpedAmount);
            yield return null;
        }
        OnWarpFinish?.Invoke();
    }
}