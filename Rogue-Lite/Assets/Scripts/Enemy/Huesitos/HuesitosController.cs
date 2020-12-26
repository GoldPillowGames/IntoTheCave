using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Enemy.Huesitos
{
    public class HuesitosController : EnemyController
    {
        #region Variables
        [SerializeField] private float velocity = 5;
        [SerializeField] private float rotationLerpValue = 15;
        [SerializeField] private float distanceToAttack = 5;
        [SerializeField] private float detectAngle;
        private Animator _anim;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public float DistanceToAttack => distanceToAttack;
        public Transform Transform => transform;
        public FiniteStateMachine StateMachine => stateMachine;
        public AnimationComboSelector AnimationAttackComboSelector { get; private set; }
        public float Velocity => velocity;
        
        public float DistanceFromPlayer => Vector3.Distance(Transform.position,
            Player.transform.position);
        public Vector3 DirectionToPlayer =>
            (Player.position - Transform.position).normalized;

        public float CurrentAnimationLength
        {
            get
            {
                var stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
                return stateInfo.length;
            }
        }
            
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
            AnimationAttackComboSelector = new AnimationComboSelector(new string[]{"IsAttacking1", "IsAttacking2"});
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
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Transform.position, Transform.position + Quaternion.Euler(0, detectAngle / 2, 0) * Transform.forward * DistanceToAttack);
            Gizmos.DrawLine(Transform.position, Transform.position + Quaternion.Euler(0, -detectAngle / 2, 0) * Transform.forward * DistanceToAttack);
        }
        
        public bool IsThereAnObstacleInAttackRange()
        {
            if (!Physics.Raycast(Transform.position, DirectionToPlayer, out var hitInfo,
                DistanceToAttack))
            {
                return false;
            }
            return !hitInfo.collider.CompareTag("Player");
        }

        public bool CanSeePlayer()
        {
            Vector2 forward = new Vector2(transform.forward.x, Transform.forward.z);
            Vector2 toPlayer = new Vector2(Player.position.x - Transform.position.x,
                Player.position.z - Transform.position.z);
            return (Vector2.Angle(forward, toPlayer) < (detectAngle / 2));
        }
        #endregion
    }
}