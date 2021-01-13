using UnityEngine;

namespace GoldPillowGames.Enemy.Litos
{
    public class LitosAnimatorEvents : MonoBehaviour
    {
        private LitosController _controller;

        private void Awake()
        {
            _controller = transform.parent.GetComponent<LitosController>();
        }

        private void Die()
        {
            _controller.DiePublic();
        }

        private void HandsDie()
        {
            _controller.HandsDie();
        }
    }
}