using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class PinchitosAnimatorEvents : MonoBehaviour
    {
        private PinchitosController _controller;

        private void Awake()
        {
            _controller = transform.parent.GetComponent<PinchitosController>();
        }

        private void GoToNextState()
        {
            _controller.GoToNextState();
        }

        private void Push(float time)
        {
            _controller.AttackPush(time);
        }

        private void Die()
        {
            _controller.DiePublic();
        }

        private void EnableStaticSpikeBall()
        {
            _controller.EnableStaticSpikeBall();
        }

        private void DisableStaticSpikeBall()
        {
            _controller.DisableStaticSpikeBall();
        }

        private void ThrowSpikeBall()
        {
            _controller.ThrowSpikeBall();
        }
        
        private void SpikeBallsOnDie()
        {
            _controller.SpikeBallsOnDie();
        }
    }
}