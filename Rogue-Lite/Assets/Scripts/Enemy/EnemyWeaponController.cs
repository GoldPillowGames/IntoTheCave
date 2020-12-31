using System.Collections.Generic;
using UnityEngine;

namespace GoldPillowGames.Enemy
{
    public class EnemyWeaponController : MonoBehaviour
    {
        private GameObject _enemy;
        private Collider _collider;
        private float _strength = 20; // Provisional, debe venir de las stats del enemigo.
        private List<GameObject> _playersHit;

        private void Awake()
        {
            _enemy = GameObject.FindObjectOfType<PlayerController>().gameObject;
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
            _playersHit = new List<GameObject>();
        }

        public void InitAttack()
        {
            _playersHit.Clear();
            _collider.enabled = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && !_playersHit.Contains(other.gameObject))
            {
                var playerController = other.GetComponent<PlayerController>();
                //playerController.ReceiveDamage(_strength, (other.transform.position - _enemy.transform.position).normalized);
                _playersHit.Add(other.gameObject);
            }
        }
    }
}