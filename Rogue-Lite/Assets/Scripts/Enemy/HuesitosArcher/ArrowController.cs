using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private float speedValue = 30;
        private int _damage = 10; // Provisional. Leer de las stats del enemigo.
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
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().TakeDamage(_damage, (other.transform.position - transform.position).normalized);
                DestroyArrow();
            }
        }

        private void DestroyArrow()
        {
            // Poner partículas etc.
            gameObject.SetActive(false);
        }
    }
}

