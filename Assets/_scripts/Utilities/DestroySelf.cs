using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float _deathTimer = 1;

    void Start()
    {
        Destroy(gameObject, _deathTimer);
    }

    
}
