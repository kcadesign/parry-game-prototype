using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarpTransition : SceneTransition
{
    public static event Action OnWarpTransitionStart;
    public static event Action OnWarpTransitionEnd;

    public GameObject WarpObject;
    private Material WarpMaterial;

    public SoundCollection _environmentSounds;

    [SerializeField] private float _transitionDuration = 3f;

    private int _waveDistanceFromCentre = Shader.PropertyToID("_WaveDistanceFromCentre");
    private int _ShockwaveStrength = Shader.PropertyToID("_ShockwaveStrength");


    private void Awake()
    {
        WarpObject.SetActive(false);

        WarpMaterial = WarpObject.GetComponent<Image>().material;
    }

    public override IEnumerator TransitionIn()
    {
        OnWarpTransitionStart?.Invoke();

        WarpObject.SetActive(true);

        _environmentSounds.PlaySound("LevelStartWarp", transform);

        float startPosition = 0;
        float endPosition = 5;

        WarpMaterial.SetFloat(_ShockwaveStrength, startPosition);
        float lerpedAmount = -0.1f;

        float elapsedTime = 0f;
        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            lerpedAmount = Mathf.Lerp(startPosition, endPosition, (elapsedTime / _transitionDuration));
            WarpMaterial.SetFloat(_ShockwaveStrength, lerpedAmount);
            yield return null;
        }
    }

    public override IEnumerator TransitionOut()
    {
        OnWarpTransitionEnd?.Invoke();

        _environmentSounds.PlaySound("LevelEndWarp", transform);


        float startPosition = 5;
        float endPosition = 0;

        WarpMaterial.SetFloat(_ShockwaveStrength, startPosition);
        float lerpedAmount = -0.1f;

        float elapsedTime = 0f;
        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            lerpedAmount = Mathf.Lerp(startPosition, endPosition, (elapsedTime / _transitionDuration));
            WarpMaterial.SetFloat(_ShockwaveStrength, lerpedAmount);
            yield return null;
        }
        WarpObject.SetActive(false);
    }
}
