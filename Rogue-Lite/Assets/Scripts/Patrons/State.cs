using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected FiniteStateMachine enemyStateMachine;

    public State(FiniteStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update(float deltaTime)
    {

    }

    public virtual void FixedUpdate(float deltaTime)
    {

    }

    public virtual void Exit()
    {

    }

}