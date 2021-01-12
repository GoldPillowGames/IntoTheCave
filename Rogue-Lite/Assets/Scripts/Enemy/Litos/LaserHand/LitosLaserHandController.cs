using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.LaserHand
{
    public class LitosLaserHandController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int laserDamage = 25;
        [SerializeField] private float timeToDamageAgain = 1;
        [SerializeField] private float velocity = 5;
        [SerializeField] private float velocityWhenLaserOn;
        [SerializeField] private float radiusToAttack = 3;
        [SerializeField] private float timeOn;
        [SerializeField] private LitosController litosController;
        [SerializeField] private ParticleSystem laserParticles;
        [SerializeField] private ParticleSystem groundParticles;
        [SerializeField] private Animator childAnim;
        private Animator _anim;
        private Collider _collider;
        private List<GameObject> _entitiesHit;

        private FiniteStateMachine _stateMachine;
        #endregion

        #region Properties

        public bool IsLaserOn { get; set; }
        public Action LaserEndCallback;
        public float Velocity => velocity;
        public float VelocityWhenLaserOn => velocityWhenLaserOn;
        public float TimeOn => timeOn;
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
        private void Awake()
        {
            _stateMachine = new FiniteStateMachine();
            _entitiesHit = new List<GameObject>();
            Player = FindObjectOfType<PlayerController>().transform;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            InitialPosition = transform.position;
            //DisableChildrenRagdoll();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate(Time.deltaTime);
        }
        
        private void Start()
        {
            _stateMachine.SetInitialState(new IdleState(this, _stateMachine, _anim));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radiusToAttack);
        }

        private void LaserStarting()
        {
            laserParticles.Play();
            groundParticles.Play();
            IsLaserOn = true;
            Invoke(nameof(LaserEnding), timeOn);
        }

        public void LaserEnding()
        {
            laserParticles.Stop();
            groundParticles.Stop();
            IsLaserOn = false;
            LaserEndCallback?.Invoke();
        }
        
        private float DistanceXZFrom(Vector3 position)
        {
            var handPosition = transform.position;
            handPosition.y = 0;
            var otherPosition = position;
            otherPosition.y = 0;
            
            return Vector3.Distance(handPosition,otherPosition);
        }
        
        /*public void SlapAttack()
        {
            var playerCollidersHit = Physics.OverlapSphere(transform.position, slapRadius,
                LayerMask.GetMask("Player")).Where(col => col.CompareTag("Player"));
            foreach (var playerCollider in playerCollidersHit)
            {
                playerCollider.GetComponent<PlayerController>().TakeDamage(laserDamage,
                    (playerCollider.transform.position - transform.position).normalized);
            }
            
            slapParticles.Play();
            
            CameraShaker.Shake(0.4f, 3, 4);
        }*/

        public void HandDie()
        {
            //EnableChildrenRagdoll();
            
            gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            foreach(Transform child in GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
            }

            _collider.enabled = false;
            _anim.enabled = false;
            enabled = false;
        }

        public void FinishAttack()
        {
            litosController.AttackFinished();
        }
        
        public void Attack()
        {
            _stateMachine.SetState(new FollowingState(this, _stateMachine, _anim));
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (IsLaserOn && other.CompareTag("Player"))
            {
                if (_entitiesHit.Contains(other.gameObject))
                {
                    return;
                }
                
                other.gameObject.GetComponent<PlayerController>().TakeDamage(laserDamage,
                    (other.transform.position - transform.position).normalized);
                
                _entitiesHit.Add(other.gameObject);
                StartCoroutine(RemoveFromEntitiesList(other.gameObject));
            }
        }
        
        private IEnumerator RemoveFromEntitiesList(GameObject entity)
        {
            yield return new WaitForSeconds(timeToDamageAgain);

            _entitiesHit.Remove(entity);
        }
        #endregion
    }
}
