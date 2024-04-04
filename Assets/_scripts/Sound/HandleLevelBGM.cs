using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLevelBGM : MonoBehaviour
{
    [Header("References")]
    public SoundCollection BGMCollection;
    private AudioSource _audioSource;

    [Header("Volume")]
    [SerializeField] private float _defaultVolume = 0.5f;
    [SerializeField] private float _pauseVolume = 0.2f;

    [Header("Fade Speed")]
    [SerializeField] private float _fadeInSpeed = 0.5f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    [Header("Level Index")]
    [SerializeField] private int _lastIntroMusicLevelIndex;
    [SerializeField] private int _lastStandardMusicLevelIndex;
    [SerializeField] private int _bossMusicLevelIndex;

    [Header("BGM Strings")]
    private string _mainMenuBGM = "MainMenu";
    private string _world1LevelBGM = "World1Level";
    private string _world1BossBGM = "World1Boss";

    public static HandleLevelBGM Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPlayerPause += GameStateManager_OnPlayerPause;
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnLevelFinish;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPlayerPause -= GameStateManager_OnPlayerPause;
        HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnLevelFinish;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // check the level index and set the audio source clip to play the appropriate music
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel <= _lastIntroMusicLevelIndex)
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_mainMenuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_mainMenuBGM).Loop;
        }
        else if (currentLevel > _lastIntroMusicLevelIndex && currentLevel < _bossMusicLevelIndex)
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1LevelBGM).Loop;
        }
        else if (currentLevel == _bossMusicLevelIndex)
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1BossBGM).Loop;
        }

        // play and fade in the audio source
        _audioSource.Play();
        StartCoroutine(FadeInBGM());
    }

    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        if (playerPaused) _audioSource.volume = _pauseVolume;
        else _audioSource.volume = _defaultVolume;
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)  _audioSource.volume = _pauseVolume;
        else _audioSource.volume = _defaultVolume;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        StartCoroutine(FadeInBGM());
        int level = scene.buildIndex;
        if (level <= _lastIntroMusicLevelIndex && _audioSource.clip != BGMCollection.FindSoundByName("MainMenu").AudioClips[0])
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_mainMenuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_mainMenuBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if ((level > _lastIntroMusicLevelIndex && level < _bossMusicLevelIndex) && _audioSource.clip != BGMCollection.FindSoundByName("World1Level").AudioClips[0])
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1LevelBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if (level == _bossMusicLevelIndex && _audioSource.clip != BGMCollection.FindSoundByName("World1Boss").AudioClips[0])
        {
            _audioSource.clip = BGMCollection.FindSoundByName(_world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1BossBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
    }

    private void HandleGameStateUI_OnStartButtonPressed()
    {
        StartCoroutine(FadeOutBGM());
    }

    private IEnumerator FadeInBGM()
    {
        while (_audioSource.volume < _defaultVolume)
        {
            _audioSource.volume += Time.deltaTime * _fadeInSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutBGM()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime * _fadeOutSpeed;
            yield return null;
        }
        _audioSource.Stop();
    }

    private IEnumerator LowerPitch()
    {
        while (_audioSource.pitch > 0.5f)
        {
            _audioSource.pitch -= Time.deltaTime * 0.1f;
            yield return null;
        }
    }
}
