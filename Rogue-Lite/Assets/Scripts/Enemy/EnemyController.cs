using System;
using System.Linq;
using GoldPillowGames.Patterns;
using UnityEngine;
using Photon.Pun;

namespace GoldPillowGames.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected float health;
        [SerializeField] protected int gold = 10;
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
            DisableChildrenRagdoll();
        }

        private void DisableChildrenRagdoll()
        {
            var childColliders = GetComponentsInChildren<Collider>().Where(col => col.gameObject != this.gameObject);
            foreach (var col in childColliders)
            {
                col.isTrigger = true;
            }
            var childRigidbodies = GetComponentsInChildren<Rigidbody>().Where(rb => rb.gameObject != this.gameObject);
            foreach (var rb in childRigidbodies)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                rb.isKinematic = true;
            }
        }
        
        private void EnableChildrenRagdoll()
        {
            var childColliders = GetComponentsInChildren<Collider>().Where(col => col.gameObject != this.gameObject);;
            foreach (var col in childColliders)
            {
                col.isTrigger = false;
            }
            var childRigidbodies = GetComponentsInChildren<Rigidbody>().Where(rb => rb.gameObject != this.gameObject);
            foreach (var rb in childRigidbodies)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                rb.isKinematic = false;
            }
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
        
        protected virtual void GiveGold()
        {
            Config.data.gold += gold;
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
            EnableChildrenRagdoll();
            
            if (_roomManager != null)
                _roomManager.EnemyDied();
            
            gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            foreach(Transform child in GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            }
        }

        virtual protected void CheckClosestPlayer()
        {

        }

        [PunRPC]
        public virtual void ReceiveDamage(float damage)
        {
            if(!Config.data.isOnline)
                health = Mathf.Max(0, health - damage);
            else
                GetComponent<PhotonView>().RPC("ReceiveDamageSync", RpcTarget.All, damage);
        }

        [PunRPC]
        public virtual void ReceiveDamageSync(float damage)
        {
            health = Mathf.Max(0, health - damage);
        }
        #endregion
    }
}

