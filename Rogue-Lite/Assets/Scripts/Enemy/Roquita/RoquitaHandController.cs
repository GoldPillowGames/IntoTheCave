using System.Collections.Generic;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class RoquitaHandController : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        private bool _isAttacking;
        private List<GameObject> _playersHit;
        private int _damage;
        
        private void Awake()
        {
            _playersHit = new List<GameObject>();
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
                playerController.TakeDamage(_damage, (other.transform.position - enemy.transform.position).normalized);
                _playersHit.Add(other.gameObject);
            }
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }
    }
}