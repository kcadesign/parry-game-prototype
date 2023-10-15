using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool _isTransitioningState = false;

    private void Start()
    {
        CurrentState.EnterState();
    }

    private void Update()
    {
        EState _nextStateKey = CurrentState.GetNextState();
        if (!_isTransitioningState && _nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!_isTransitioningState)
        {
            TransitionToState(_nextStateKey);
        }
        CurrentState.UpdateState();
    }

    public void TransitionToState(EState stateKey)
    {
        _isTransitioningState = true;
        CurrentState.EnterState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        _isTransitioningState = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentState.OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentState.OnTriggerExit2D(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentState.OnCollisionEnter2D(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CurrentState.OnCollisionStay2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CurrentState.OnCollisionExit2D(collision);
    }

}
*/