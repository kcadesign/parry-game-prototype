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

    [Header("BGM Strings")]
    private string _menuBGM = "MainMenu";
    private string _storyScreenBGM = "Story";
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
        //HandleGameStateUI.OnStartButtonPressed += HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        //HandleGameStateUI.OnStartButtonPressed -= HandleGameStateUI_OnStartButtonPressed;
        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // check the level name type and set the audio source clip to play the appropriate music
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.StartsWith("Menu"))
        {
            // play menu music
            _audioSource.clip = BGMCollection.FindSoundByName(_menuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_menuBGM).Loop;
            _audioSource.volume = 0;
        }
        else if (scene.name.StartsWith("Story"))
        {
            // play story screen music
            _audioSource.clip = BGMCollection.FindSoundByName(_storyScreenBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_storyScreenBGM).Loop;
            _audioSource.volume = 0;
        }
        else if (scene.name.StartsWith("World1"))
        {
            // play world 1 music
            _audioSource.clip = BGMCollection.FindSoundByName(_world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1LevelBGM).Loop;
            _audioSource.volume = 0;
        }
        else if (scene.name.StartsWith("Boss1"))
        {
            // play boss music
            _audioSource.clip = BGMCollection.FindSoundByName(_world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1BossBGM).Loop;
            _audioSource.volume = 0;
        }
        // play and fade in the audio source whichever level you start in
        _audioSource.Play();
        StartCoroutine(FadeInBGM());
        //Debug.Log("Playing audio source clip: " + _audioSource.clip.name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // check the level name type and set the audio source clip to play the appropriate music
        // music only changes if logic is met
        if (scene.name.StartsWith("Menu") && _audioSource.clip != BGMCollection.FindSoundByName("MainMenu").AudioClips[0])
        {
            // play menu music
            _audioSource.clip = BGMCollection.FindSoundByName(_menuBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_menuBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if (scene.name.StartsWith("Story") && _audioSource.clip != BGMCollection.FindSoundByName("Story").AudioClips[0])
        {
            // play story screen music
            _audioSource.clip = BGMCollection.FindSoundByName(_storyScreenBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_storyScreenBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if (scene.name.StartsWith("World1") && _audioSource.clip != BGMCollection.FindSoundByName("World1Level").AudioClips[0])
        {
            // play world 1 music
            _audioSource.clip = BGMCollection.FindSoundByName(_world1LevelBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1LevelBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        else if (scene.name.StartsWith("Boss1") && _audioSource.clip != BGMCollection.FindSoundByName("World1Boss").AudioClips[0])
        {
            // play boss music
            _audioSource.clip = BGMCollection.FindSoundByName(_world1BossBGM).AudioClips[0];
            _audioSource.loop = BGMCollection.FindSoundByName(_world1BossBGM).Loop;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeInBGM());
        }
        //Debug.Log("Playing audio source clip: " + _audioSource.clip.name);
    }

    private void GameStateManager_OnPlayerPause(bool playerPaused)
    {
        if (playerPaused) _audioSource.volume = _pauseVolume;
        else _audioSource.volume = _defaultVolume;
    }

/*    private void HandleGameStateUI_OnStartButtonPressed()
    {
        StartCoroutine(FadeOutBGM());
    }
*/
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
