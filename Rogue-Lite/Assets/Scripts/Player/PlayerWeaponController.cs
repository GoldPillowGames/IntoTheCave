using System.Collections.Generic;
using GoldPillowGames.Enemy;
using UnityEngine;

namespace GoldPillowGames.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private GameObject _player;
        private bool _isAttacking = false;
        private List<GameObject> _enemiesHit;

        private float _strength = 20; // Provisional, debe venir de las stats del jugador.
        private float _pushForce = 4; // Provisional, debe venir de las stats del jugador.
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
            _enemiesHit = new List<GameObject>();
        }

        public void InitAttack()
        {
            _enemiesHit.Clear();
            _isAttacking = true;
        }

        public void FinishAttack()
        {
            _isAttacking = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isAttacking && other.CompareTag("Enemy") && !_enemiesHit.Contains(other.gameObject))
            {
                var enemyController = other.GetComponent<EnemyController>();
                enemyController.ReceiveDamage(_strength);
                enemyController.Push(0.5f, _pushForce, (other.transform.position - _player.transform.position).normalized);
                _enemiesHit.Add(other.gameObject);
            }
        }
    }
}
