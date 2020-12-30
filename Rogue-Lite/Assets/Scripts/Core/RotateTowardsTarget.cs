using UnityEngine;

namespace GoldPillowGames.Core
{
    public class RotateTowardsTarget
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly float _rotationSpeed;
 
        private Quaternion _lookRotation;
        private Vector3 _direction;

        public RotateTowardsTarget(Transform self, Transform target, float rotationSpeed)
        {
            this._self = self;
            _target = target;
            _rotationSpeed = rotationSpeed;
        }

        public void Update(float deltaTime)
        {
            _direction = (_target.position - _self.position).normalized;
 
            _lookRotation = Quaternion.LookRotation(_direction);
 
            _self.rotation = Quaternion.Slerp(_self.rotation, _lookRotation, deltaTime * _rotationSpeed);
        }
    }
}