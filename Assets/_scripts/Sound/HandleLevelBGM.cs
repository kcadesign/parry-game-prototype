using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLevelBGM : MonoBehaviour
{
    [Header("References")]
    public SoundCollection BGMCollection;
    private AudioSource _audioSource;

    [Header("Volume")]
    [SerializeField] private float _defaultVolume = 0.5f;
    [SerializeField] private float _lowVolume = 0.2f;

    [Header("Fade Speed")]
    [SerializeField] private float _fadeSpeed = 0.5f;

    [Header("BGM Strings")]
    private string _menuBGM = "MainMenu";
    private string _storyScreenBGM = "Story";
    private string _world1LevelBGM = "World1Level";
    private string _world1BossBGM = "World1Boss";
    private string _gameOverBGM = "GameOver";

    private int _maxHealth;

    public static HandleLevelBGM Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Start()
    {
        // Start the BGM when the game starts
        string initialClipName = GetBGMClipNameForScene(SceneManager.GetActiveScene().name);
        if (initialClipName != null)
        {
            AudioClip initialClip = BGMCollection.FindSoundByName(initialClipName)?.AudioClips[0];
            if (initialClip != null)
            {
                _audioSource.clip = initialClip;
                StartCoroutine(FadeInBGM());
            }
        }
    }

    private void OnEnable()
    {
        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded += OnSceneLoaded;
        HandlePlayerHealth.OnHealthInitialise += HandlePlayerHealth_OnHealthInitialise;
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandlePlayerHealth.OnPlayerDead += HandlePlayerDeath_OnPlayerDead;
    }

    private void OnDisable()
    {
        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPlayerPause;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        HandlePlayerHealth.OnHealthInitialise -= HandlePlayerHealth_OnHealthInitialise;
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandlePlayerHealth.OnPlayerDead -= HandlePlayerDeath_OnPlayerDead;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        string newClipName = GetBGMClipNameForScene(scene.name);
        if (newClipName != null)
        {
            AudioClip newClip = BGMCollection.FindSoundByName(newClipName)?.AudioClips[0];
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
        StartCoroutine(ChangeVolume(playerPaused ? _lowVolume : _defaultVolume));
    }

    private void HandlePlayerHealth_OnHealthInitialise(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        StartCoroutine(ChangeVolume(currentHealth < _maxHealth * 0.25f ? _lowVolume : _defaultVolume));
    }

    private void HandlePlayerDeath_OnPlayerDead()
    {
        AudioClip gameOverClip = BGMCollection.FindSoundByName(_gameOverBGM)?.AudioClips[0];
        if (gameOverClip != null)
        {
            StartCoroutine(CrossFadeBGM(gameOverClip));
        }
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        float startVolume = _audioSource.volume;
        float time = 0;

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / _fadeSpeed);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        _audioSource.volume = targetVolume;
    }

    private IEnumerator FadeInBGM()
    {
        _audioSource.Play();
        yield return ChangeVolume(_defaultVolume);
    }

    private IEnumerator FadeOutBGM()
    {
        yield return ChangeVolume(0);
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
}
