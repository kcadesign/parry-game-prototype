using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss1BaseState
{
    public abstract void EnterState(Boss1StateManager boss);
    public abstract void UpdateState(Boss1StateManager boss);
    public abstract void SwitchState(Boss1StateManager boss);
}
