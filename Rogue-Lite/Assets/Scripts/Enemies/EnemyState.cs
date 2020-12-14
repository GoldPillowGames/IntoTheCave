using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyBehaviour enemyBehaviour;
        protected EnemyStateMachine enemyStateMachine;

        public EnemyState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine)
        {
            this.enemyBehaviour = enemyBehaviour;
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
}

