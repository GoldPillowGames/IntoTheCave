using System;
using GoldPillowGames.Patterns;
using UnityEngine;
using Photon.Pun;

namespace GoldPillowGames.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected float health;

        public float Health => health;
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

        protected virtual void Die()
        {
            
        }
        
        public virtual void ReceiveDamage(float damage)
        {
            if(!Config.data.isOnline)
                health = Mathf.Max(0, health - damage);
            else
                GetComponent<PhotonView>().RPC("ReceiveDamageSync", RpcTarget.All, damage);
            // Update health bar.
        }

        [PunRPC]
        public virtual void ReceiveDamageSync(float damage)
        {
            health = Mathf.Max(0, health - damage);
        }
        #endregion
    }
}

