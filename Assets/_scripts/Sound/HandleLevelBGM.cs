using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLevelBGM : MonoBehaviour
{
    [SerializeField] private AudioSource _levelBGM;
    [SerializeField] private float _desiredVolume = 0.5f;
    [SerializeField] private float _fadeInSpeed = 0.5f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    private void Awake()
    {
        _levelBGM = GetComponent<AudioSource>();
        _levelBGM.volume = 0;
    }

    /*private void OnEnable()
    {
        HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
    }*/

    private void Start()
    {
        // slowly fade in the main menu music
        StartCoroutine(FadeInMainMenuBGM());
    }

    /*private void OnDisable()
    {
        HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
    }*/

/*    private void HandleGameStateUI_OnStartButtonPressed()
    {
        // slowly fade out the main menu music
        StartCoroutine(FadeOutMainMenuBGM());
    }
*/
    private IEnumerator FadeInMainMenuBGM()
    {
        while (_levelBGM.volume < _desiredVolume)
        {
            _levelBGM.volume += Time.deltaTime * _fadeInSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutMainMenuBGM()
    {
        while (_levelBGM.volume > 0)
        {
            _levelBGM.volume -= Time.deltaTime * _fadeOutSpeed;
            yield return null;
        }
        _levelBGM.Stop();
    }

}
