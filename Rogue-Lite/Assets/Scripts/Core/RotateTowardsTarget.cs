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
            _self = self;
            _target = target;
            _rotationSpeed = rotationSpeed;
        }

        public void Update(float deltaTime)
        {
            _direction = (_target.position - _self.position).normalized;
 
            _lookRotation = Quaternion.LookRotation(_direction);

            /*if (Quaternion.Angle(_self.rotation, _lookRotation) < 15)
            {
                return;
            }*/
            
            _self.rotation = Quaternion.Slerp(_self.rotation, _lookRotation, deltaTime * _rotationSpeed);
        }
    }
}