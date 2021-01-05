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
        protected FiniteStateMachine stateMachine;
        private RoomManager _roomManager;
        public Action GoToNextStateCallback { set; private get; }
        
        public float Health => health;
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            stateMachine = new FiniteStateMachine();
            _roomManager = FindObjectOfType<RoomManager>(); // Make Singleton.
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

        [PunRPC]
        public virtual void Push(float time, float force, Vector3 direction)
        {
            
        }

        protected virtual void Die()
        {
            if (_roomManager != null)
                _roomManager.EnemyDied();
        }

        [PunRPC]
        public virtual void ReceiveDamage(float damage)
        {
            //if(!Config.data.isOnline)
            health = Mathf.Max(0, health - damage);
            //else
            //    GetComponent<PhotonView>().RPC("ReceiveDamageSync", RpcTarget.All, damage);
        }

        [PunRPC]
        public virtual void ReceiveDamageSync(float damage)
        {
            health = Mathf.Max(0, health - damage);
        }
        #endregion
    }
}

