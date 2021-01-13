using System;
using System.Linq;
using GoldPillowGames.Patterns;
using UnityEngine;
using Photon.Pun;

namespace GoldPillowGames.Enemy.Litos.SlapHand
{
    public class LitosSlapHandController : EnemyController
    {
        #region Variables
        [SerializeField] private int slapDamage = 25;
        [SerializeField] private float velocity = 5;
        [SerializeField] private float radiusToAttack = 3;
        [SerializeField] private float slapRadius = 5;
        [SerializeField] private ParticleSystem slapParticles;
        [SerializeField] private LitosController litosController;
        [SerializeField] private Animator childAnim;
        private Animator _anim;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private PhotonView photonView;
        private PlayerController[] _players;
        #endregion

        #region Properties
        public float Velocity => velocity;
        public Transform Player { get; private set; }
        public Animator ChildAnim => childAnim;
        public Vector3 InitialPosition { get; private set; }

        private Vector3 DirectionToPlayer
        {
            get
            {
                var direction = Player.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                
                return direction;
            }
        }
            

        public bool PlayerIsInRange => DistanceXZFrom(Player.position) <= radiusToAttack;
        public bool IsClosedToInitPosition => DistanceXZFrom(InitialPosition) <= 1;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            photonView = GetComponent<PhotonView>();
            _players = FindObjectsOfType<PlayerController>();
            Player = FindObjectOfType<PlayerController>().transform;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            InitialPosition = transform.position;
            
        }

        protected override void Start()
        {
            base.Start();

            health = int.MaxValue;
            if (!photonView.IsMine && Config.data.isOnline)
                return;

            stateMachine.SetInitialState(new IdleState(this, stateMachine, _anim));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radiusToAttack);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, slapRadius);
        }
        
        private float DistanceXZFrom(Vector3 position)
        {
            var handPosition = transform.position;
            handPosition.y = 0;
            var otherPosition = position;
            otherPosition.y = 0;
            
            return Vector3.Distance(handPosition,otherPosition);
        }
        
        public void SlapAttack()
        {
            _collider.isTrigger = false;
            var playerCollidersHit = Physics.OverlapSphere(transform.position, slapRadius,
                LayerMask.GetMask("Player")).Where(col => col.CompareTag("Player"));
            foreach (var playerCollider in playerCollidersHit)
            {
                playerCollider.GetComponent<PlayerController>().TakeDamage(slapDamage,
                    (playerCollider.transform.position - transform.position).normalized);
            }
            
            slapParticles.Play();
            
            CameraShaker.Shake(0.4f, 3, 4);
        }

        [PunRPC]
        public void HandDie()
        {
            EnableChildrenRagdoll();
            
            gameObject.layer = LayerMask.NameToLayer("DeathEnemy");

            foreach(Transform child in GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            }

            _collider.enabled = true;
            _collider.isTrigger = false;
            _anim.enabled = false;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            
            stateMachine.SetState(new IdleState(this, stateMachine, _anim));
            ChildAnim.SetBool("IsAttacking", true);
            
            enabled = false;
        }

        public void FinishAttack()
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            litosController.AttackFinished();
        }
        
        public void Attack()
        {
            if (!photonView.IsMine && Config.data.isOnline)
                return;
            stateMachine.SetState(new FollowingState(this, stateMachine, _anim));
        }
        
        [PunRPC]
        public override void ReceiveDamage(float damage)
        {
            litosController.ReceiveDamage(damage);
        }
        #endregion
    }
}
