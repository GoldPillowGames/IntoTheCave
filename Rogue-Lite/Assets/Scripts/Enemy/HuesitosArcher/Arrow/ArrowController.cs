using System;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher.Arrow
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private float speedValue = 30;
        
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 position, Vector3 direction)
        {
            transform.position = position;

            transform.forward = direction;

            _rigidbody.velocity = direction * speedValue;
        }
    }
}

