using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager TransitionManagerInstance;

    public GameObject TransitionContainer;
    private SceneTransition[] _transitions;

    private void Awake()
    {
        if (TransitionManagerInstance == null)
        {
            TransitionManagerInstance = this;
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
        // Find the specified transition
        SceneTransition transition = _transitions.First(t => t.name == transitionName);

        // Start loading the scene asynchronously
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // Start transition-in animation
        yield return transition.TransitionIn();

        // Wait until the scene is almost done loading
        while (scene.progress < 0.9f)
        {
            yield return null;
        }

        // Allow the scene to activate
        scene.allowSceneActivation = true;

        // Wait for the scene to fully load
        while (!scene.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Start transition-out animation
        yield return transition.TransitionOut();
    }
}
