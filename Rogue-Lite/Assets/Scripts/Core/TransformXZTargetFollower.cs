using UnityEngine;

namespace GoldPillowGames.Core
{
    public class TransformXZTargetFollower
    {
        #region Variables
        private readonly Transform _transform;
        private readonly float _velocity;
        #endregion

        #region Methods
        public TransformXZTargetFollower(Transform transform, float velocity)
        {
            _transform = transform;
            _velocity = velocity;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            var direction = targetPosition - _transform.position;
            direction.y = 0;
            direction.Normalize();

            _transform.position += direction * (_velocity * Time.deltaTime);
        }
        #endregion
    }
}