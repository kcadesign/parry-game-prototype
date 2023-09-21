using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDamage
{
    public event Action<GameObject> OnDamage; 

    
}
