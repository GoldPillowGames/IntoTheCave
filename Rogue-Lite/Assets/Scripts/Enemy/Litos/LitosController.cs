using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Litos.SlapHand;
using GoldPillowGames.Enemy.Pinchitos;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GoldPillowGames.Enemy.Litos
{
    public class LitosController : EnemyController
    {
        #region Variables
        [SerializeField] private LitosSlapHandController slapHand;
        [SerializeField] private float timeBetweenAttacks = 0.7f;
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
            
            DoNewAttack();
        }
        
        protected override void Die()
        {
            base.Die();

            slapHand.HandDie();
            
            _collider.enabled = false;
            _anim.enabled = false;
            enabled = false;
        }

        public void DiePublic()
        {
            Die();
        }

        private void DoNewAttack()
        {
            if (Random.value >= 0.5f)
            {
                slapHand.Attack();
            }
            else
            {
                slapHand.Attack(); // Poner la otra mano.
            }
        }

        public void AttackFinished()
        {
            Invoke(nameof(DoNewAttack), timeBetweenAttacks);
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