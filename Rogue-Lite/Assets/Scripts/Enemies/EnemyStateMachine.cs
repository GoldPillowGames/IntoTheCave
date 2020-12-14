using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine
    {
        protected EnemyState currentState;

        public EnemyStateMachine()
        {

        }

        public void SetInitialState(EnemyState initialState)
        {
            currentState = initialState;
            currentState.Enter();
        }

        public void SetState(EnemyState incomingState)
        {
            currentState.Exit();
            currentState = incomingState;
            incomingState.Enter();
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
}

