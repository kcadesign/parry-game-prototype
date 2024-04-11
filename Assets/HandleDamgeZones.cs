using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamgeZones : MonoBehaviour
{
    [Header("Triggers")]
    public CheckTriggerEntered TriggerLeft;
    public CheckTriggerEntered TriggerRight;

    [Header("Negative Zones")]
    public GameObject NegativeZoneLeft;
    public GameObject NegativeZoneRight;

    public float ActivateDelay = 2f;
    public float ActiveZoneMoveSpeed = 1f;

    private Vector3 _zoneLeftStartPosition;
    private Vector3 _zoneRightStartPosition;

    private void Start()
    {
        TriggerLeft.SetAttackDelay(ActivateDelay);
        TriggerRight.SetAttackDelay(ActivateDelay);

        _zoneLeftStartPosition = NegativeZoneLeft.transform.position;
        _zoneRightStartPosition = NegativeZoneRight.transform.position;
    }

    private void Update()
    {
        if (TriggerLeft.ActivateBehaviour && !TriggerRight.ActivateBehaviour)
        {
            NegativeZoneLeft.SetActive(true);
            MoveZone(NegativeZoneLeft, new Vector3(-10, 0, 0));
        }
        else if (!TriggerLeft.ActivateBehaviour)
        {
            NegativeZoneLeft.SetActive(false);
            ResetPosition(NegativeZoneLeft, _zoneLeftStartPosition);
        }

        if (TriggerRight.ActivateBehaviour && !TriggerLeft.ActivateBehaviour)
        {
            NegativeZoneRight.SetActive(true);
            MoveZone(NegativeZoneRight, new Vector3(10, 0, 0));
        }
        else if (!TriggerRight.ActivateBehaviour)
        {
            NegativeZoneRight.SetActive(false);
            ResetPosition(NegativeZoneRight, _zoneRightStartPosition);
        }
    }

    private void MoveZone(GameObject zone, Vector3 targetPosition)
    {
        zone.transform.position = Vector3.MoveTowards(zone.transform.position, targetPosition, ActiveZoneMoveSpeed * Time.deltaTime);
    }

    private void ResetPosition(GameObject zone, Vector3 originalPosition)
    {
        zone.transform.position = originalPosition;
    }
}
