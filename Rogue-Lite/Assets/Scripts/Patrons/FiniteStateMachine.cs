using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    protected State currentState;

    public FiniteStateMachine()
    {

    }

    public void SetInitialState(State initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void SetState(State incomingState)
    {
        currentState.Exit();
        currentState = incomingState;
        currentState.Enter();
    }

    public void Update(float deltaTime)
    {
        currentState.Update(deltaTime);
    }

    public void FixedUpdate(float deltaTime)
    {
        currentState.FixedUpdate(deltaTime);
    }
}

