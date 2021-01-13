using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Litos.LaserHand;
using GoldPillowGames.Enemy.Litos.SlapHand;
using GoldPillowGames.Enemy.Pinchitos;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Photon.Pun;
using Photon.Realtime;

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
        private PhotonView photonView;
        private PlayerController[] _players;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            photonView = GetComponent<PhotonView>();

            _players = FindObjectsOfType<PlayerController>();
            _anim = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();

            if (!photonView.IsMine && Config.data.isOnline)
                return;
            
            transform.forward = -Vector3.forward;
            
            //if (!photonView.IsMine && Config.data.isOnline)
            //{
            //    Agent.enabled = false;
            //}
        }

        protected override void Start()
        {
            base.Start();

            if (!photonView.IsMine && Config.data.isOnline)
                return;

            stateMachine.SetInitialState(new IdleState(this, stateMachine, _anim));
            
            Invoke(nameof(DoNewAttack), timeBetweenAttacks);
        }

        [PunRPC]
        protected override void Die()
        {
            base.Die();
            
            _collider.enabled = false;
            _anim.enabled = false;
            enabled = false;

            if (!Config.data.isOnline)
            {
                Config.data.dungeonsCompleted++;
                FindObjectOfType<PlayerController>().health = -10000;
            }
            else
            {

            }
        }

        public void DiePublic()
        {
            Die();
        }

        [PunRPC]
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

        protected override void Update()
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            base.Update();
        }

        protected override void FixedUpdate()
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            base.FixedUpdate();
        }

        public void AttackFinished()
        {
            Invoke(nameof(DoNewAttack), timeBetweenAttacks);
        }

        [PunRPC]
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