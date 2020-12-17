using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyState : State
    {
        protected EnemyBehaviour enemyBehaviour;
        public EnemyState(EnemyBehaviour enemyBehaviour, FiniteStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }
    }
}