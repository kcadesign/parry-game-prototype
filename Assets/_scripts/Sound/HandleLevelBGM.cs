using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;
using static GameStateManager;

public class HandleLevelBGM : MonoBehaviour
{
    [Header("References")]
    public SoundCollection BGMCollection;
    private AudioSource _audioSource;

    [Header("Volume")]
    [SerializeField] private float _desiredVolume = 0.5f;
    [SerializeField] private float _pauseVolume = 0.2f;

    [Header("Fade Speed")]
    [SerializeField] private float _fadeInSpeed = 0.5f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    public static HandleLevelBGM Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPlayerPause += GameStateManager_OnPlayerPause;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPlayerPause -= GameStateManager_OnPlayerPause;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // check the level index and set the audio source clip to play the appropriate music
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 0)
        {
            string mainMenuBGM = "MainMenu";
            _audioSource.clip = BGMCollection.FindSoundByName(mainMenuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(mainMenuBGM).Loop;
        }
        else if (currentLevel == 1 || currentLevel == 2 || currentLevel == 3)
        {
            string world1LevelBGM = "World1Level";
            _audioSource.clip = BGMCollection.FindSoundByName(world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(world1LevelBGM).Loop;
        }
        else if (currentLevel == 4)
        {
            string world1BossBGM = "World1Boss";
            _audioSource.clip = BGMCollection.FindSoundByName(world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(world1BossBGM).Loop;
        }

        // play and fade in the audio source
        _audioSource.Play();
        StartCoroutine(FadeInBGM());
    }

    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        if (playerPaused)
        {
            _audioSource.volume = _pauseVolume;
        }
        else
        {
            _audioSource.volume = _desiredVolume;
        }
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            _audioSource.volume = _pauseVolume;
        }
        else
        {
            _audioSource.volume = _desiredVolume;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        StartCoroutine(FadeInBGM());
        int level = scene.buildIndex;
        if (level == 0 && _audioSource.clip != BGMCollection.FindSoundByName("MainMenu").AudioClips[0])
        {
            string mainMenuBGM = "MainMenu";
            _audioSource.clip = BGMCollection.FindSoundByName(mainMenuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(mainMenuBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if ((level == 1 || level == 2 || level == 3) && _audioSource.clip != BGMCollection.FindSoundByName("World1Level").AudioClips[0])
        {
            string world1LevelBGM = "World1Level";
            _audioSource.clip = BGMCollection.FindSoundByName(world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(world1LevelBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if (level == 4 && _audioSource.clip != BGMCollection.FindSoundByName("World1Boss").AudioClips[0])
        {
            string world1BossBGM = "World1Boss";
            _audioSource.clip = BGMCollection.FindSoundByName(world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(world1BossBGM).Loop;
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
        while (_audioSource.volume < _desiredVolume)
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
