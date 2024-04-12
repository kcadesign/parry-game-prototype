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
        if (TriggerLeft.TriggerActive && !TriggerRight.TriggerActive)
        {
            MoveZone(NegativeZoneLeft, new Vector3(-10, 0, 0));
        }
        else if (!TriggerLeft.TriggerActive)
        {
            ResetZone(NegativeZoneLeft, _zoneLeftStartPosition);
        }

        if (TriggerRight.TriggerActive && !TriggerLeft.TriggerActive)
        {
            MoveZone(NegativeZoneRight, new Vector3(10, 0, 0));
        }
        else if (!TriggerRight.TriggerActive)
        {
            ResetZone(NegativeZoneRight, _zoneRightStartPosition);
        }
    }

    private void MoveZone(GameObject zone, Vector3 targetPosition)
    {
        zone.SetActive(true);
        zone.transform.position = Vector3.MoveTowards(zone.transform.position, targetPosition, ActiveZoneMoveSpeed * Time.deltaTime);
    }

    private void ResetZone(GameObject zone, Vector3 originalPosition)
    {
        //zone.transform.position = originalPosition;
        // move back towards original position
        zone.transform.position = Vector3.MoveTowards(zone.transform.position, originalPosition, ActiveZoneMoveSpeed * Time.deltaTime);

        // if we are close enough to the original position, deactivate the zone
        if (Vector3.Distance(zone.transform.position, originalPosition) < 0.1f)
        {
            zone.SetActive(false);
        }
    }
}
