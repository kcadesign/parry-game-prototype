using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    public event Action<GameObject> OnDamageCollision; 

    
}
