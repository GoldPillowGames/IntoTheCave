using System;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Enemy.Huesitos
{
    public class HuesitosController : EnemyController
    {
        #region Variables
        [SerializeField] private HuesitosWeaponController weapon;
        [SerializeField] private float velocity = 5;
        [SerializeField] private float distanceToAttack = 5;
        [SerializeField] private float detectAngle;
        [SerializeField] private float pushComboForce = 15;
        [SerializeField] private float timeDefenseless = 1;
        private Animator _anim;
        private AgentPropeller _propeller;
        private Collider _collider;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Transform Transform => transform;
        public float Velocity => velocity;
        public float TimeDefenseless => timeDefenseless;
        public Action<int> OnComboHitEnding { get; set; }

        private float DistanceFromPlayer => Vector3.Distance(Transform.position,
            Player.transform.position);

        private Vector3 DirectionToPlayer =>
            (Player.position - Transform.position).normalized;

        public bool PlayerIsInRange => DistanceFromPlayer <= distanceToAttack;
        public bool CanAttack => (PlayerIsInRange && CanSeePlayer() && !IsThereAnObstacleInAttackRange());
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            Agent = GetComponent<NavMeshAgent>();
            // Player = GameObject.FindGameObjectWithTag("Player").transform;
            Player = FindObjectOfType<PlayerController>().transform;
            _anim = GetComponent<Animator>();
            _propeller = new AgentPropeller(Agent);
            _collider = GetComponent<Collider>();
        }

        protected override void Start()
        {
            base.Start();
            
            stateMachine.SetInitialState(new FollowingState(this, stateMachine, _anim));
            transform.forward = DirectionToPlayer;
        }

        protected override void Update()
        {
            base.Update();
            
            _propeller.Update(Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceToAttack);
            
            Gizmos.color = Color.blue;
            var position = Transform.position;
            var forward = Transform.forward;
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, detectAngle / 2, 0) * forward * distanceToAttack);
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, -detectAngle / 2, 0) * forward * distanceToAttack);
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

        private bool CanSeePlayer()
        {
            Vector2 forward = new Vector2(transform.forward.x, Transform.forward.z);
            var playerPosition = Player.position;
            var selfPosition = Transform.position;
            
            Vector2 toPlayer = new Vector2(playerPosition.x - selfPosition.x,
                playerPosition.z - selfPosition.z);
            
            return (Vector2.Angle(forward, toPlayer) < (detectAngle / 2));
        }

        private void SetNextComboAnimationID(int hitIndex)
        {
            OnComboHitEnding?.Invoke(hitIndex);
        }
        
        private void PushCombo(float time)
        {
            Push(time, pushComboForce, transform.forward);
        }

        public override void Push(float time, float force, Vector3 direction)
        {
            _propeller.StartPush(time, force * direction);
        }

        protected override void Die()
        {
            base.Die();
            
            gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            _collider.enabled = false;
            Agent.enabled = false;
            _anim.enabled = false;
            weapon.Disable();
            enabled = false;
        }

        public override void ReceiveDamage(float damage)
        {
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
        
        private void InitAttackInWeapon()
        {
            weapon.InitAttack();
        }

        private void FinishAttackInWeapon()
        {
            weapon.FinishAttack();
        }
        #endregion
    }
}