using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterfaceCollision : MonoBehaviour, IDamager, IDamageable
{
    public event Action<GameObject> OnDamageCollision;

}
