using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController
{
    public event Action<bool, bool> OnHandleState;
}
