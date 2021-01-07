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
        
        [SerializeField] protected int gold = 10;
        protected FiniteStateMachine stateMachine;
        private RoomManager _roomManager;
        protected float maxHealth;
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
            maxHealth = health;
            // Perk -> Less initial life
            health -= health * FindObjectOfType<PlayerStatus>().lessInitialLifeForEnemies / 100;
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
            if(!Config.data.isOnline)
                Config.data.gold += gold + FindObjectOfType<PlayerStatus>().goldPerEnemy;
            else
                Config.data.gold += (gold / 2) + FindObjectOfType<PlayerStatus>().goldPerEnemy;
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

        virtual protected void CheckClosestPlayer()
        {

        }

        [PunRPC]
        public virtual void ReceiveDamage(float damage)
        {
            if (!Config.data.isOnline)
            {
                if(health - damage <= maxHealth * FindObjectOfType<PlayerStatus>().enemiesThreshold)
                {
                    health = 0;
                }
                else
                {
                    health = Mathf.Max(0, health - damage);
                }
            }
            else
                GetComponent<PhotonView>().RPC("ReceiveDamageSync", RpcTarget.All, damage);
        }

        [PunRPC]
        public virtual void ReceiveDamageSync(float damage)
        {
            if (health - damage <= maxHealth * FindObjectOfType<PlayerStatus>().enemiesThreshold)
            {
                health = 0;
            }
            else
            {
                health = Mathf.Max(0, health - damage);
            }
        }
        #endregion
    }
}

