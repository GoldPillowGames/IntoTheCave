using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldPillowGames.Enemy.Huesitos
{
    public class HuesitosWeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        private bool _isAttacking;
        private List<GameObject> _playersHit;
        private MeshCollider _collider;
        private Rigidbody _rigidbody;
        private int _damage;

        private Vector3 _initialLocalPosition;
        private Quaternion _initialLocalRotation;
        
        private void Awake()
        {
            _playersHit = new List<GameObject>();
            _collider = GetComponent<MeshCollider>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider.enabled = false;
            _initialLocalPosition = transform.localPosition;
            _initialLocalRotation = transform.localRotation;
        }

        public void InitAttack()
        {
            _playersHit.Clear();
            _isAttacking = true;
        }
        
        public void FinishAttack()
        {
            _isAttacking = false;
        }

        private void Update()
        {
            transform.localPosition = _initialLocalPosition;
            transform.localRotation = _initialLocalRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isAttacking && other.CompareTag("Player") && !_playersHit.Contains(other.gameObject))
            {
                var playerController = other.GetComponent<PlayerController>();
                playerController.TakeDamage(_damage, (other.transform.position - enemy.transform.position).normalized);
                _playersHit.Add(other.gameObject);
            }
        }

        public void Disable()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public void SetDamage(int comboAttackDamage)
        {
            _damage = comboAttackDamage;
        }
    }
}