using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLevelProgression : MonoBehaviour
{
    public delegate void SendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator);
    public static event SendCurrentCheckpoint OnSendCurrentCheckpoint;

    private Transform currentCheckpoint;
    public GameObject[] CheckpointArray;

    // Initialize the current checkpoint to the starting position
    void Awake()
    {
        currentCheckpoint = transform.GetChild(0);
    }

    void Start()
    {
        PopulateCheckpointArray();
    }

    private void PopulateCheckpointArray()
    {
        Transform parentTransform = gameObject.transform;

        CheckpointArray = new GameObject[parentTransform.childCount];

        int checkpointIndex = 0;
        foreach (Transform childTransform in parentTransform)
        {
            CheckpointArray[checkpointIndex] = childTransform.gameObject;
            checkpointIndex++;
        }
        /*
        foreach (GameObject child in CheckpointArray)
        {
            Debug.Log("Child Name: " + child.name);
        }
        */
    }

    public void SetCurrentCheckpoint(Transform checkpoint, GameObject checkpointActivator)
    {
        currentCheckpoint = checkpoint;
        OnSendCurrentCheckpoint?.Invoke(GetRespawnPoint(), checkpointActivator);
    }

    public Vector3 GetRespawnPoint()
    {
        return currentCheckpoint.position;
    }
}
