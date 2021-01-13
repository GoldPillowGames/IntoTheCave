using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Litos.LaserHand;
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
        [SerializeField] private LitosLaserHandController laserHand;
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
            transform.forward = -Vector3.forward;
            
            Invoke(nameof(DoNewAttack), timeBetweenAttacks);
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

        public void HandsDie()
        {
            laserHand.HandDie();
            slapHand.HandDie();
        }
        
        private void DoNewAttack()
        {
            if (Random.value >= 0.5f)
            {
                laserHand.Attack();
            }
            else
            {
                slapHand.Attack();
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