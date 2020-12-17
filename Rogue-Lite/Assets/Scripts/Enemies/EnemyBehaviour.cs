using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.ExampleEnemy;

namespace Enemy
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected FiniteStateMachine stateMachine;

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
    }
}

