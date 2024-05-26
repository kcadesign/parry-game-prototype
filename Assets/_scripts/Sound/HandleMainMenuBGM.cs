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
        //_mainMenuAudioSource = GetComponent<AudioSource>();
        Debug.Log($"Main menu audio source: {_mainMenuAudioSource}");
    }

    private void OnEnable()
    {
        //HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
    }

    private void OnDisable()
    {
        //HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
    }

    private void Start()
    {
        _mainMenuAudioSource.volume = 0;
        _mainMenuAudioSource.Play();
        Debug.Log("Playing main menu music...");

        // slowly fade in the main menu music
        StartCoroutine(FadeInMainMenuBGM());
    }


/*    private void HandleGameStateUI_OnStartButtonPressed()
    {
        // slowly fade out the main menu music
        StartCoroutine(FadeOutMainMenuBGM());
    }
*/
    private IEnumerator FadeInMainMenuBGM()
    {
        Debug.Log("Fade in coroutine started");

        while (_mainMenuAudioSource.volume < 1)
        {
            Debug.Log("Entered while loop");
            _mainMenuAudioSource.volume += Time.deltaTime * _fadeInSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutMainMenuBGM()
    {
        Debug.Log("Fade out coroutine started");
        while (_mainMenuAudioSource.volume > 0)
        {
            _mainMenuAudioSource.volume -= Time.deltaTime * _fadeOutSpeed;
            yield return null;
        }
        _mainMenuAudioSource.Stop();
    }
}
