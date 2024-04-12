using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLevelStart : MonoBehaviour
{
    public GameObject WarpObject;
    private Material LevelFinishWarpMaterial;

    [SerializeField] private float _shockwaveTime = 3f;
    private int _waveDistanceFromCentre = Shader.PropertyToID("_WaveDistanceFromCentre");
    private int _ShockwaveStrength = Shader.PropertyToID("_ShockwaveStrength");

    private void Start()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex > 2 && currentLevelIndex < 8)
        {
            WarpObject.SetActive(true);
            StartCoroutine(LevelWarp(5, 0));
        }
    }

    private IEnumerator LevelWarp(float startPosition, float endPosition)
    {
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
        WarpObject.SetActive(false);

    }

}
