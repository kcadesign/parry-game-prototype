using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandleState
{
    public event Action<Enum> OnHandleState;
}
