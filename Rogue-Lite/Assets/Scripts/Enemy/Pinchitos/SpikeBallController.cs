using System;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class SpikeBallController : MonoBehaviour
    {
        [SerializeField] private float velocity = 20.0f;
        private PinchitosController _pinchitosController;
        private bool _isInTheAir;
        private int _damage;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private Transform _initialParent;
        private Vector3 _initialLocalPosition;
        private Animator _anim;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
            _pinchitosController = GetComponentInParent<PinchitosController>();
            _collider.isTrigger = true;
            _initialParent = transform.parent;
            _initialLocalPosition = transform.localPosition;
        }

        private void Start()
        {
            _rigidbody.isKinematic = false;
        }

        public void InitThrow(Vector3 target)
        {
            _isInTheAir = true;
            transform.parent = null;
            var direction = target - transform.position;
            direction.y = -1;
            _rigidbody.velocity = direction.normalized * velocity;
            _pinchitosController.CanThrow = false;
        }

        private void FinishThrow()
        {
            _isInTheAir = false;
            _rigidbody.velocity = Vector3.zero;
            _anim.Play("Respawn");
        }

        private void Respawn()
        {
            transform.parent = _initialParent;
            transform.localPosition = _initialLocalPosition;
        }
        
        private void CanThrowAgain()
        {
            _pinchitosController.CanThrow = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isInTheAir)
            {
                return;
            }

            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<PlayerController>();
                playerController.TakeDamage(_damage, (other.transform.position - transform.position).normalized);
                FinishThrow();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("KEK");
                FinishThrow();
            }
        }

        public void OnEnemyDie()
        {
            _rigidbody.useGravity = true;
            _collider.isTrigger = false;
            transform.parent = null;
        }

        public void SetDamage(int comboAttackDamage)
        {
            _damage = comboAttackDamage;
        }
    }
}
