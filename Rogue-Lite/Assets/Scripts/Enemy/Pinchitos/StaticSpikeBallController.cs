using System.Collections.Generic;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class StaticSpikeBallController : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool _isAttacking;
        private List<GameObject> _playersHit;
        private int _damage;
        
        private void Awake()
        {
            _playersHit = new List<GameObject>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
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

        private void OnTriggerEnter(Collider other)
        {
            if (_isAttacking && other.CompareTag("Player") && !_playersHit.Contains(other.gameObject))
            {
                var playerController = other.GetComponent<PlayerController>();
                Debug.Log(_damage);
                playerController.TakeDamage(_damage, (other.transform.position - enemy.transform.position).normalized);
                _playersHit.Add(other.gameObject);
            }
        }

        public void OnEnemyDie()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _collider.isTrigger = false;
            transform.parent = null;
        }
        
        public void SetDamage(int damage)
        {
            _damage = damage;
        }
    }
}
