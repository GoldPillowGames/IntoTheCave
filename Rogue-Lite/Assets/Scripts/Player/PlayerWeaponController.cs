using System.Collections.Generic;
using GoldPillowGames.Enemy;
using UnityEngine;

namespace GoldPillowGames.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private GameObject _player;
        private Collider _collider;
        private float _strength = 20; // Provisional, debe venir de las stats del jugador.
        private float _pushForce = 10; // Provisional, debe venir de las stats del jugador.
        private List<GameObject> _enemiesHit;

        private void Awake()
        {
            _player = GameObject.FindObjectOfType<PlayerController>().gameObject;
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
            _enemiesHit = new List<GameObject>();
        }

        public void InitAttack()
        {
            _enemiesHit.Clear();
            _collider.enabled = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && !_enemiesHit.Contains(other.gameObject))
            {
                var enemyController = other.GetComponent<EnemyController>();
                enemyController.ReceiveDamage(_strength);
                enemyController.Push(0.5f, _pushForce, (other.transform.position - _player.transform.position).normalized);
                _enemiesHit.Add(other.gameObject);
            }
        }
    }
}
