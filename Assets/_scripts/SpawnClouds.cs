using System.Collections;
using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    public GameObject CloudPrefab;

    void Start()
    {
        StartCoroutine(SpawnCloud());
    }

    IEnumerator SpawnCloud()
    {
        while (true)
        {
            // Generate random local position within a range
            Vector3 localSpawnPosition = new Vector3(0, Random.Range(-10, 0), 0);
            // Convert local position to world position
            Vector3 spawnPosition = transform.TransformPoint(localSpawnPosition);

            float spawnRate = Random.Range(0.1f, 5);
            // Use parent's rotation for cloud rotation
            Quaternion spawnRotation = transform.rotation;

            Instantiate(CloudPrefab, spawnPosition, spawnRotation, transform);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
