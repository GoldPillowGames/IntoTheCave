using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Pinchitos;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GoldPillowGames.Enemy.Litos
{
    public class LitosController : EnemyController
    {
        #region Variables
        [SerializeField] private int attackSlapDamage = 25;
        [SerializeField] private int attackLaserDamage = 22;
        private Animator _anim;
        private Collider _collider;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            _anim = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
        }

        protected override void Start()
        {
            base.Start();
            
            stateMachine.SetInitialState(new IdleState(this, stateMachine, _anim));
        }
        
        protected override void Die()
        {
            base.Die();

            _collider.enabled = false;
            _anim.enabled = false;
            enabled = false;
        }

        public void DiePublic()
        {
            Die();
        }
        
        public override void ReceiveDamage(float damage)
        {
            base.ReceiveDamage(damage);
            
            if (health <= 0)
            {
                stateMachine.SetState(new DeathState(this, stateMachine, _anim));
            }
        }
        #endregion
    }
}