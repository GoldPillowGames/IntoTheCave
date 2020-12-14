using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.ExampleEnemy;

namespace Enemy
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected EnemyStateMachine stateMachine;

        protected virtual void Awake()
        {
            stateMachine = new EnemyStateMachine();
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

