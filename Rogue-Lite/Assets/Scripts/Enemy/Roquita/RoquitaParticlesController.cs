using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class RoquitaParticlesController : MonoBehaviour
    {
        private ParticleSystem _particles;
        private float _height;

        private void Awake()
        {
            _particles = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            Physics.Raycast(transform.position, Vector3.down, out var hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground"));
            _height = hitInfo.point.y;
            transform.parent = null;
        }

        public void Play(Vector3 roquitaPosition)
        {
            transform.position = new Vector3(roquitaPosition.x, _height, roquitaPosition.z);
            _particles.Play();
        }

        public void Stop()
        {
            _particles.Stop();
        }
    }
}
