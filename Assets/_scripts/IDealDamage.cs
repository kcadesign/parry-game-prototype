using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDamage
{
    public event Action OnDamage; // Event for player damage handling

    // Add any other common collision-related methods or events here
    // For example:
    // event Action OnDeflect; // Event for deflecting
}
