using System;
using GoldPillowGames.Core;
using GoldPillowGames.Enemy.HuesitosArcher.Arrow;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class HuesitosArcherController : EnemyController
    {
        #region Variables
        [SerializeField] private float velocity = 5;
        [SerializeField] private float distanceToAttack = 5;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject visualArrow;
        private Animator _anim;
        private AgentPropeller _propeller;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        
        public NavMeshAgent Agent { get; private set; }
        public Transform Transform => transform;
        public float Velocity => velocity;
        public GameObject VisualArrow => visualArrow;
        public Action GoToNextStateCallback { set; private get; }
        private float DistanceFromPlayer => Vector3.Distance(Transform.position,
            Player.transform.position);
        private Vector3 DirectionToPlayer =>
            (Player.position - Transform.position).normalized;
        private bool PlayerIsInRange => DistanceFromPlayer <= distanceToAttack;
        public bool CanAttack => (PlayerIsInRange /*&& !IsThereAnObstacleInAttackRange()*/);
        public float RotationSpeed => rotationSpeed;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            Agent = GetComponent<NavMeshAgent>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            _anim = GetComponent<Animator>();
            _propeller = new AgentPropeller(Agent);
        }

        protected override void Start()
        {
            base.Start();
            
            stateMachine.SetInitialState(new FollowingState(this, stateMachine, _anim));
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceToAttack);
        }

        private bool IsThereAnObstacleInAttackRange()
        {
            if (!Physics.Raycast(Transform.position, DirectionToPlayer, out var hitInfo,
                distanceToAttack))
            {
                return false;
            }
            return !hitInfo.collider.CompareTag("Player");
        }

        private void GoToNextState()
        {
            GoToNextStateCallback?.Invoke();
        }

        private void ShowArrow()
        {
            visualArrow.SetActive(true);
        }

        private void ThrowArrow()
        {
            var arrow = ObjectPool.Instance.GetObject(arrowPrefab);
            // BUG: visual.arrow.position no siempre devuelve la posición exacta del objeto, lo que hace que la flecha se origine mal a veces (ni idea de por qué ocurre).
            arrow.GetComponent<ArrowController>().Init(visualArrow.transform.position, transform.forward);
            visualArrow.SetActive(false);
        }
        #endregion
    }
}