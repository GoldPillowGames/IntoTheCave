using System;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Photon.Pun;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class HuesitosArcherController : EnemyController
    {
        #region Variables
        [SerializeField] private float velocity = 5;
        [SerializeField] private float distanceToAttack = 5;
        [SerializeField] private float distanceToStopAttacking = 8;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float timeDefenseless = 1;
        [SerializeField] private float minTimeToFire = 1;
        [SerializeField] private float maxTimeToFire = 1.5f;
        [SerializeField] private BowController bowController;
        private Animator _anim;
        private AgentPropeller _propeller;
        private BoxCollider _collider;
        private PhotonView photonView;
        private PlayerController[] _players;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        
        public NavMeshAgent Agent { get; private set; }
        public Transform Transform => transform;
        public float Velocity => velocity;
        public float RotationSpeed => rotationSpeed;
        public float TimeDefenseless => timeDefenseless;
        public float TimeToFire => Random.Range(minTimeToFire, maxTimeToFire);
        private float DistanceFromPlayer => Vector3.Distance(Transform.position,
            Player.transform.position);
        private Vector3 DirectionToPlayer =>
            (Player.position - Transform.position).normalized;
        private bool PlayerIsInRange => DistanceFromPlayer <= distanceToAttack;
        public bool CanAttack => (PlayerIsInRange && !IsThereAnObstacleInAttackRange());
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            photonView = GetComponent<PhotonView>();
            _players = FindObjectsOfType<PlayerController>();
            Agent = GetComponent<NavMeshAgent>();
            // Player = GameObject.FindGameObjectWithTag("Player").transform;
            Player = FindObjectOfType<PlayerController>().transform;
            _anim = GetComponent<Animator>();
            _propeller = new AgentPropeller(Agent);
            _collider = GetComponent<BoxCollider>();

            if (!photonView.IsMine && Config.data.isOnline)
            {
                Agent.enabled = false;
            }
        }

        protected override void Start()
        {
            base.Start();

            if (!photonView.IsMine && Config.data.isOnline)
                return;

            if (Config.data.isOnline)
            {
                CheckClosestPlayer();
            }

            stateMachine.SetInitialState(new FollowingState(this, stateMachine, _anim));
            transform.forward = DirectionToPlayer;
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

            if (Config.data.isOnline)
            {
                CheckClosestPlayer();
            }

            _propeller.Update(Time.deltaTime);
        }

        protected override void CheckClosestPlayer()
        {
            base.CheckClosestPlayer();

            if (_players.Length < 2)
                _players = FindObjectsOfType<PlayerController>();

            float distance = Mathf.Infinity;
            PlayerController closestPlayer = null;

            foreach (PlayerController p in _players)
            {
                if (p != null)
                {
                    if ((p.transform.position - transform.position).magnitude < distance)
                    {
                        distance = (p.transform.position - transform.position).magnitude;
                        closestPlayer = p;
                    }
                }
            }

            if (closestPlayer != null)
            {
                if (Player != closestPlayer)
                {
                    Player = closestPlayer.transform;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceToAttack);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanceToStopAttacking);
        }

        private bool IsThereAnObstacleInAttackRange()
        {
            if (!Physics.Raycast(Transform.position, DirectionToPlayer, out var hitInfo,
                distanceToAttack, LayerMask.GetMask("Ground", "Player")))
            {
                return false;
            }
            return !hitInfo.collider.CompareTag("Player");
        }

        private void ShowVisualArrow()
        {
            bowController.ShowVisualArrow();
        }

        public void HideVisualArrow()
        {
            bowController.HideVisualArrow();
        }
        
        private void ThrowArrow()
        {
            bowController.ThrowArrow();
        }

        [Photon.Pun.PunRPC]
        public override void Push(float time, float force, Vector3 direction)
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            _propeller.StartPush(time, force * direction);
        }
        
        protected override void Die()
        {
            base.Die();
            
            gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            }
            _collider.enabled = false;
            Agent.enabled = false;
            _anim.enabled = false;
            bowController.Disable();
            enabled = false;
        }
        
        [Photon.Pun.PunRPC]
        public override void ReceiveDamage(float damage)
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;

            base.ReceiveDamage(damage);

            if (health > 0)
            {
                stateMachine.SetState(new HurtState(this, stateMachine, _anim));
            }
            else
            {
                stateMachine.SetState(new DeathState(this, stateMachine, _anim));
            }
        }

        #endregion
    }
}