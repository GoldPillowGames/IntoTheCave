using UnityEngine;

namespace GoldPillowGames.Player
{
    public class CrossbowArrowController : MonoBehaviour
    {
        [SerializeField] private float speed = 30;
        private int _damage = 10; // Cambiar por daño del player.
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
                case "Player":
                    other.GetComponent<PlayerController>().TakeDamage(_damage, (other.transform.position - transform.position).normalized);
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