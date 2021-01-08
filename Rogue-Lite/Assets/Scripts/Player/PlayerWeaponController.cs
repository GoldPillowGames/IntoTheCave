using System;
using System.Collections.Generic;
using GoldPillowGames.Enemy;
using UnityEngine;

namespace GoldPillowGames.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        private bool _isAttacking;
        private List<GameObject> _enemiesHit;
        
        private void Awake()
        {
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
                if (!Config.data.isOnline)
                {
                    enemyController.ReceiveDamage(player.damage);
                    enemyController.Push(0.5f, player.push, (other.transform.position - player.transform.position).normalized);
                }
                else
                {
                    enemyController.GetComponent<Photon.Pun.PhotonView>().RPC("ReceiveDamage", Photon.Pun.RpcTarget.All, (float)player.damage);
                    enemyController.GetComponent<Photon.Pun.PhotonView>().RPC("Push", Photon.Pun.RpcTarget.All, 0.5f, player.push, (other.transform.position - player.transform.position).normalized);
                }
                
                _enemiesHit.Add(other.gameObject);
            }
        }

        private void OnEnable()
        {
            player.SetNewCurrentWeapon(this);
        }
    }
}
