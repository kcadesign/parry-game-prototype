using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMainMenuBGM : MonoBehaviour
{
    [SerializeField] private AudioSource _mainMenuAudioSource;
    [SerializeField] private float _fadeInSpeed = 0.5f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    private void Awake()
    {
        _mainMenuAudioSource = GetComponent<AudioSource>();
        _mainMenuAudioSource.volume = 0;
    }

    private void OnEnable()
    {
        HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
    }

    private void Start()
    {
        // slowly fade in the main menu music
        StartCoroutine(FadeInMainMenuBGM());
    }

    private void OnDisable()
    {
        HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
    }

    private void HandleGameStateUI_OnStartButtonPressed()
    {
        // slowly fade out the main menu music
        StartCoroutine(FadeOutMainMenuBGM());
    }

    private IEnumerator FadeInMainMenuBGM()
    {
        while (_mainMenuAudioSource.volume < 1)
        {
            _mainMenuAudioSource.volume += Time.deltaTime * _fadeInSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutMainMenuBGM()
    {
        while (_mainMenuAudioSource.volume > 0)
        {
            _mainMenuAudioSource.volume -= Time.deltaTime * _fadeOutSpeed;
            yield return null;
        }
        _mainMenuAudioSource.Stop();
    }
}
