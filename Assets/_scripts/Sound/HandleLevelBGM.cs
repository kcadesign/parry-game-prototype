using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLevelBGM : MonoBehaviour
{
    [Header("References")]
    public SoundCollection BGMCollection;
    private AudioSource _audioSource;
    private AudioClip _preloadedGameOverClip;

    [Header("Volume")]
    [SerializeField] private float _defaultVolume = 0.5f;
    [SerializeField] private float _pauseVolume = 0.2f;

    [Header("Fade Speed")]
    [SerializeField] private float _fadeInSpeed = 0.5f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    [Header("BGM Strings")]
    private string _menuBGM = "MainMenu";
    private string _storyScreenBGM = "Story";
    private string _world1LevelBGM = "World1Level";
    private string _world1BossBGM = "World1Boss";
    private string _gameOverBGM = "GameOver";

    public static HandleLevelBGM Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;

        // Preload the game over audio clip
        _preloadedGameOverClip = BGMCollection.FindSoundByName(_gameOverBGM)?.AudioClips[0];
    }

    private void OnEnable()
    {
        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded += OnSceneLoaded;
        HandlePlayerHealth.OnPlayerDead += HandlePlayerDeath_OnPlayerDead;
    }

    private void OnDisable()
    {
        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        HandlePlayerHealth.OnPlayerDead -= HandlePlayerDeath_OnPlayerDead;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        _audioSource.volume = _defaultVolume;
        string newClipName = GetBGMClipNameForScene(scene.name);
        if (newClipName != null)
        {
            var newClip = BGMCollection.FindSoundByName(newClipName)?.AudioClips[0];
            if (newClip != null && _audioSource.clip != newClip)
            {
                StartCoroutine(CrossFadeBGM(newClip));
            }
        }
    }

    private string GetBGMClipNameForScene(string sceneName)
    {
        if (sceneName.StartsWith("Menu")) return _menuBGM;
        if (sceneName.StartsWith("Story")) return _storyScreenBGM;
        if (sceneName.StartsWith("World1")) return _world1LevelBGM;
        if (sceneName.StartsWith("Boss1")) return _world1BossBGM;
        return null;
    }

    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        _audioSource.volume = playerPaused ? _pauseVolume : _defaultVolume;
    }

    private void HandlePlayerDeath_OnPlayerDead()
    {
        if (_preloadedGameOverClip != null)
        {
            StartCoroutine(CrossFadeBGM(_preloadedGameOverClip));
        }
    }

    private IEnumerator FadeInBGM()
    {
        _audioSource.volume = 0;
        _audioSource.Play();
        while (_audioSource.volume < _defaultVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime * _fadeInSpeed;
            _audioSource.volume = Mathf.Clamp(_audioSource.volume, 0, _defaultVolume);
            yield return null;
        }
    }

    private IEnumerator FadeOutBGM()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.unscaledDeltaTime * _fadeOutSpeed;
            _audioSource.volume = Mathf.Clamp(_audioSource.volume, 0, _defaultVolume);
            yield return null;
        }
        _audioSource.Stop();
    }

    private IEnumerator CrossFadeBGM(AudioClip newClip)
    {
        if (_audioSource.isPlaying)
        {
            yield return FadeOutBGM();
        }
        _audioSource.clip = newClip;
        yield return FadeInBGM();
    }

    private IEnumerator LowerPitch()
    {
        while (_audioSource.pitch > 0.5f)
        {
            _audioSource.pitch -= Time.unscaledDeltaTime * 0.1f;
            yield return null;
        }
    }
}
