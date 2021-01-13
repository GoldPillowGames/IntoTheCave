using GoldPillowGames.Enemy;
using UnityEngine;

namespace GoldPillowGames.Player
{
    public class CrossbowArrowController : MonoBehaviour
    {
        [SerializeField] private float speed = 30;
        [SerializeField] private float weaponPush = 3;
        [SerializeField] private AudioClip[] impactClip;
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Init(Vector3 position, Vector3 direction)
        {
            transform.position = position;

            transform.forward = direction;

            _rigidbody.velocity = direction * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Enemy":
                    var enemyController = other.GetComponent<EnemyController>();
                    CameraShaker.Shake(0.1f, 1.75f, 2f);

                    PlayerStatus player = GetComponentInParent<PlayerStatus>();
                    DamageText.Spawn((int)player.damage, other.ClosestPoint(transform.position));
                    player.GetComponent<PlayerController>().health += player.damage * (int)player.healthSteal / 100 * (int)player.heal;
                    if(player.GetComponent<PlayerController>().health > player.health)
                    {
                        player.GetComponent<PlayerController>().health = player.health;
                    }

                    if (impactClip.Length != 0)
                    {
                        print("Sound");
                        Audio.PlaySFX(impactClip[UnityEngine.Random.Range(0, impactClip.Length)], 0.4f);
                    }


                    if (!Config.data.isOnline)
                    {
                        enemyController.ReceiveDamage(player.damage / 2);
                        enemyController.Push(0.5f, weaponPush* player.push, (other.transform.position - player.transform.position).normalized);
                    }
                    else
                    {
                        enemyController.GetComponent<Photon.Pun.PhotonView>().RPC("ReceiveDamage", Photon.Pun.RpcTarget.All, (float)player.damage);
                        enemyController.GetComponent<Photon.Pun.PhotonView>().RPC("Push", Photon.Pun.RpcTarget.All, 0.5f, weaponPush* player.push, (other.transform.position - player.transform.position).normalized);
                    }
                    DestroyArrow();
                    break;
                case "Ground":
                    DestroyArrow();
                    break;
            }
        }

        private void DestroyArrow()
        {
            // Put particles, etc.
            gameObject.SetActive(false);
        }
    }
}