using System.Linq;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class PinchitosController : EnemyController
    {
        #region Variables
        [SerializeField] private float velocity = 5;
        [SerializeField] private float distanceToAttack = 5;
        [SerializeField] private float detectAngle;
        [SerializeField] private float pushAttackForce = 15;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float minTimeToThrowSpikeBall = 1.0f;
        [SerializeField] private float maxTimeToThrowSpikeBall = 4.0f;
        [SerializeField] private int attackMelee1Damage = 20;
        [SerializeField] private int attackMelee21Damage = 12;
        [SerializeField] private int attackMelee22Damage = 16;
        [SerializeField] private int attackCannonDamage = 18;
        private SpikeBallController _spikeBall;
        private StaticSpikeBallController _staticSpikeBall;
        private Animator _anim;
        private AgentPropeller _propeller;
        private Collider _collider;
        private PhotonView photonView;
        private PlayerController[] _players;
        #endregion

        #region Properties
        public Transform Player { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public float Velocity => velocity;
        public float RotationSpeed => rotationSpeed;
        public bool CanRotate { get; set; }
        public bool HasDied { get; set; }
        public bool CanThrow { get; set; } = true;

        public float TimeToThrowSpikeBall => Random.Range(minTimeToThrowSpikeBall, maxTimeToThrowSpikeBall);
        private float DistanceFromPlayer => Vector3.Distance(transform.position,
            Player.transform.position);
        private Vector3 DirectionToPlayer =>
            (Player.position - transform.position).normalized;

        public bool PlayerIsInRange => DistanceFromPlayer <= distanceToAttack;
        public bool CanAttack => (PlayerIsInRange && CanSeePlayer() && !IsThereAnObstacleInAttackRange());
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            photonView = GetComponent<PhotonView>();

            _players = FindObjectsOfType<PlayerController>();

            Agent = GetComponent<NavMeshAgent>();
            Player = FindObjectOfType<PlayerController>().transform;
            _anim = GetComponentInChildren<Animator>();
            _propeller = new AgentPropeller(Agent);
            _collider = GetComponent<Collider>();

            _spikeBall = GetComponentInChildren<SpikeBallController>();
            _staticSpikeBall = GetComponentInChildren<StaticSpikeBallController>();

            if (!photonView.IsMine && Config.data.isOnline)
            {
                Agent.enabled = false;
            }
        }

        protected override void Start()
        {
            _spikeBall.SetDamage(attackCannonDamage);

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

        protected override void CheckClosestPlayer()
        {
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
            
            _propeller.Update(Time.deltaTime);
        }

        public void ThrowSpikeBall()
        {
            CanRotate = false;
            _spikeBall.InitThrow(Player.position);
        }

        public void SpikeBallsOnDie()
        {
            _staticSpikeBall.OnEnemyDie();
            _spikeBall.OnEnemyDie();
        }
        
        public void EnableStaticSpikeBall()
        {
            _staticSpikeBall.InitAttack();
        }

        public void DisableStaticSpikeBall()
        {
            _staticSpikeBall.FinishAttack();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceToAttack);
            
            Gizmos.color = Color.blue;
            var position = transform.position;
            var forward = transform.forward;
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, detectAngle / 2, 0) * forward * distanceToAttack);
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, -detectAngle / 2, 0) * forward * distanceToAttack);
        }

        private bool IsThereAnObstacleInAttackRange()
        {
            if (!Physics.Raycast(transform.position, DirectionToPlayer, out var hitInfo,
                distanceToAttack, LayerMask.GetMask("Ground", "Player")))
            {
                return false;
            }
            return !hitInfo.collider.CompareTag("Player");
        }
        
        public bool IsThereAnObstacleInFrontOfPlayer()
        {
            if (!Physics.Raycast(transform.position, DirectionToPlayer, out var hitInfo,
                Mathf.Infinity, LayerMask.GetMask("Ground", "Player")))
            {
                return false;
            }
            return !hitInfo.collider.CompareTag("Player");
        }

        private bool CanSeePlayer()
        {
            Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
            var playerPosition = Player.position;
            var selfPosition = transform.position;
            
            Vector2 toPlayer = new Vector2(playerPosition.x - selfPosition.x,
                playerPosition.z - selfPosition.z);
            
            return (Vector2.Angle(forward, toPlayer) < (detectAngle / 2));
        }
        
        public void AttackPush(float time)
        {
            _propeller.StartPush(time, pushAttackForce * transform.forward);
        }

        public void SetMelee1Damage()
        {
            if (!Config.data.isOnline)
                _staticSpikeBall.SetDamage(attackMelee1Damage);
            else
                photonView.RPC("SetDamage", RpcTarget.All, attackMelee1Damage);
        }

        [PunRPC]
        public void SetDamage(int damage)
        {
            _staticSpikeBall.SetDamage(attackMelee1Damage);
        }

        
        public void SetMelee21Damage()
        {
            //_staticSpikeBall.SetDamage(attackMelee21Damage);
            if (!Config.data.isOnline)
                _staticSpikeBall.SetDamage(attackMelee21Damage);
            else
                photonView.RPC("SetDamage", RpcTarget.All, attackMelee21Damage);
        }
        
        public void SetMelee22Damage()
        {
            //_staticSpikeBall.SetDamage(attackMelee22Damage);
            if (!Config.data.isOnline)
                _staticSpikeBall.SetDamage(attackMelee22Damage);
            else
                photonView.RPC("SetDamage", RpcTarget.All, attackMelee22Damage);
        }
        
        protected override void Die()
        {
            base.Die();

            HasDied = true;
            _collider.enabled = false;
            Agent.enabled = false;
            _anim.enabled = false;
            enabled = false;

            if (!Config.data.isOnline)
            {
                GiveGold();
            }
            else
            {
                if (photonView.IsMine)
                {
                    photonView.RPC("GiveGold", RpcTarget.All);
                }
            }
        }

        [PunRPC]
        protected override void GiveGold()
        {
            base.GiveGold();
        }

        public void DiePublic()
        {
            Die();
        }


        [PunRPC]
        public override void ReceiveDamage(float damage)
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            base.ReceiveDamage(damage);
            
            if (health <= 0)
            {
                stateMachine.SetState(new DeathState(this, stateMachine, _anim));
            }
        }
        
        public void ResetState(AnimatedState state)
        { 
            var currentState = _anim.GetCurrentAnimatorStateInfo(0);
            var stateName = currentState.fullPathHash;
            
            _anim.Play(stateName,0, 0.0f);
            
            stateMachine.SetState(state);
        }
        #endregion
    }
}