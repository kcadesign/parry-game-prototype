using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform Platform;
    public Transform[] PlatformWaypoints;
    [SerializeField] private float _platformSpeed = 1f;
    [SerializeField] private float _waitTime = 1f;

    private int _currentWaypointIndex = 0;
    private bool _isMoving = true;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        Vector2 target = PlatformWaypoints[_currentWaypointIndex].position;
        Platform.transform.position = Vector2.MoveTowards(Platform.transform.position, target, _platformSpeed * Time.fixedDeltaTime);

        float distance = Vector2.Distance(Platform.transform.position, target);
        if (distance < 0.1f)
        {
            StartCoroutine(StopAtWaypoint());
            _currentWaypointIndex = (_currentWaypointIndex + 1) % PlatformWaypoints.Length;
        }
    }

    private IEnumerator StopAtWaypoint()
    {
        _isMoving = false;
        yield return new WaitForSeconds(_waitTime);
        _isMoving = true;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < PlatformWaypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(PlatformWaypoints[i].position, PlatformWaypoints[i + 1].position);
        }

        // Draw line from last waypoint to the first waypoint to make it a loop
        Gizmos.DrawLine(PlatformWaypoints[PlatformWaypoints.Length - 1].position, PlatformWaypoints[0].position);
    }
}
