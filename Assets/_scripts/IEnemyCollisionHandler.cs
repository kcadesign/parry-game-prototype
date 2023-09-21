using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCollisionHandler
{
    public event Action OnDamagePlayer; // Event for player damage handling

    // Add any other common collision-related methods or events here
    // For example:
    // event Action OnDeflect; // Event for deflecting
}
