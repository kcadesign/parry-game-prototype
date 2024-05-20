using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    public GameObject TransitionContainer;
    private SceneTransition[] _transitions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _transitions = TransitionContainer.GetComponentsInChildren<SceneTransition>();
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        SceneTransition transition = _transitions.First(t => t.name == transitionName);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        yield return transition.TransitionIn();
        scene.allowSceneActivation = true;
        yield return transition.TransitionOut();
    }
}
