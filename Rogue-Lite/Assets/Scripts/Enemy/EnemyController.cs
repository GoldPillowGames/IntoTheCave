using System;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected float health;
        protected FiniteStateMachine stateMachine;
        public Action GoToNextStateCallback { set; private get; }
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

        protected void GoToNextState()
        {
            GoToNextStateCallback?.Invoke();
        }

        public virtual void Push(float time, float force, Vector3 direction)
        {
            
        }
        
        public virtual void ReceiveDamage(float damage)
        {
            health = Mathf.Max(0, damage);
            // Update health bar.
        }
        #endregion
    }
}

