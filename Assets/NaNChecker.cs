using UnityEngine;

public class NaNChecker : MonoBehaviour
{
    public static NaNChecker NaNCheckerInstance;

    private void Awake()
    {
        if (NaNCheckerInstance == null)
        {
            NaNCheckerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        foreach (Transform t in FindObjectsOfType<Transform>())
        {
            if (float.IsNaN(t.position.x) || float.IsNaN(t.position.y) || float.IsNaN(t.position.z) ||
                float.IsNaN(t.rotation.x) || float.IsNaN(t.rotation.y) || float.IsNaN(t.rotation.z) ||
                float.IsNaN(t.localScale.x) || float.IsNaN(t.localScale.y) || float.IsNaN(t.localScale.z))
            {
                Debug.LogError($"NaN detected in GameObject: {t.name}");
            }
        }
    }
}
