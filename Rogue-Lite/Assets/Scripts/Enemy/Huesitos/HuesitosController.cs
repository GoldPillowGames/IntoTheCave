using System;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

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
        [SerializeField] private int[] comboAttackDamages = {15, 20};
        private Animator _anim;
        private AgentPropeller _propeller;
        private Collider _collider;
        private PhotonView photonView;
        private PlayerController[] _players;
        
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
            // currentEmissiveColor = 

            photonView = GetComponent<PhotonView>();

            _players = FindObjectsOfType<PlayerController>();
            Agent = GetComponent<NavMeshAgent>();
            // Player = GameObject.FindGameObjectWithTag("Player").transform;
            Player = FindObjectOfType<PlayerController>().transform;
            _propeller = new AgentPropeller(Agent);
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider>();

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

        protected override void CheckClosestPlayer()
        {
            if (_players.Length < 2)
                _players = FindObjectsOfType<PlayerController>();

            float distance = Mathf.Infinity;
            PlayerController closestPlayer = null;

            foreach (PlayerController p in _players)
            {
                if(p != null)
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
           

            if (Config.data.isOnline)
            {
                CheckClosestPlayer();
            }

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
            foreach(Transform child in GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            }
            _collider.enabled = false;
            Agent.enabled = false;
            _anim.enabled = false;
            weapon.transform.parent = null;
            weapon.Disable();
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

        public void SetComboAttackDamageFromIndex(int comboIndex)
        {
            weapon.SetDamage(comboAttackDamages[comboIndex]);
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