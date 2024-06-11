using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DetectInputDevice : MonoBehaviour
{
    public static event Action<string> OnControlSchemeChanged;
    public static DetectInputDevice Instance { get; private set; }

    public string CurrentControlScheme { get; private set; }

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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnControlSchemeChanged?.Invoke(CurrentControlScheme);
    }

    public void SwitchControls(PlayerInput input)
    {
        CurrentControlScheme = input.currentControlScheme;
        Debug.Log($"Switched to {CurrentControlScheme}");
        OnControlSchemeChanged?.Invoke(CurrentControlScheme);
    }
}
