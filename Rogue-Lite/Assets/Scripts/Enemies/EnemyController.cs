using System.Collections;
using System.Collections.Generic;
using Enemy.ExampleEnemy;
using Patterns.State;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        #region Variables
        protected FiniteStateMachine stateMachine;
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            stateMachine = new FiniteStateMachine();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            stateMachine.Update(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.FixedUpdate(Time.deltaTime);
        }
        #endregion
    }
}

