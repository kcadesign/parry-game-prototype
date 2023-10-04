using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParryable
{
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

}
