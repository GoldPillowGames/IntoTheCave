using System;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Enemy.ZombieKnight
{
    public class ZombieKnightController : EnemyController
    {
        #region Variables
        [SerializeField] private float velocity = 5;
        [SerializeField] private float rotationLerpValue = 15;
        [SerializeField] private float distanceToAttack = 5;
        private Animator _anim;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public float DistanceToAttack => distanceToAttack;
        public Transform Transform => transform;
        public FiniteStateMachine StateMachine => stateMachine;
        
        public float DistanceFromPlayer => Vector3.Distance(Transform.position,
            Player.transform.position);
        public Vector3 DirectionToPlayer =>
            (Player.position - Transform.position).normalized;
        public float CurrentAnimationLength => _anim.GetCurrentAnimatorStateInfo(0).length;
        public bool CanAttack => DistanceFromPlayer <= DistanceToAttack;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            _anim = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();

            Agent.speed = velocity;
            Agent.isStopped = true;
            
            stateMachine.SetInitialState(new FollowingState(this, stateMachine, _anim));
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceToAttack);
        }
        
        public bool IsThereAnObstacleInAttackRange()
        {
            Physics.Raycast(Transform.position, DirectionToPlayer, out var hitInfo,
                DistanceToAttack);
            return !hitInfo.collider.CompareTag("Player");
        }
        #endregion
    }
}