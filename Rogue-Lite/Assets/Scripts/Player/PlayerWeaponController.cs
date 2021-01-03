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
        
        private void OnTriggerStay(Collider other)
        { 
            if (_isAttacking && other.CompareTag("Enemy") && !_enemiesHit.Contains(other.gameObject))
            {
                var enemyController = other.GetComponent<EnemyController>();
                CameraShaker.Shake(0.1f, 1.75f, 2f);
                PlayerStatus player = GetComponentInParent<PlayerStatus>();
                DamageText.Spawn((int)player.damage, other.ClosestPoint(transform.position));
                enemyController.ReceiveDamage(player.damage);
                enemyController.Push(0.5f, _pushForce * player.push, (other.transform.position - _player.transform.position).normalized);
                _enemiesHit.Add(other.gameObject);
            }
        }
    }
}
