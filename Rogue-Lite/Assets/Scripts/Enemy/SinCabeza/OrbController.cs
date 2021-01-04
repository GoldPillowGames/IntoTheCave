using UnityEngine;

namespace GoldPillowGames.Enemy.SinCabeza
{
    public class OrbController : MonoBehaviour
    {
        [SerializeField] private float speedValue = 20;
        [SerializeField] private int damage = 15;
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

            _rigidbody.velocity = direction * speedValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Player":
                    other.GetComponent<PlayerController>().TakeDamage(damage, (other.transform.position - transform.position).normalized);
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

