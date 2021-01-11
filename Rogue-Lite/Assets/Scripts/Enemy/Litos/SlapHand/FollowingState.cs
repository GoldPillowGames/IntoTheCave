using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Roquita;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.SlapHand
{
    public class FollowingState : AnimatedState
    {
        #region Variables
        private readonly LitosSlapHandController _handController;
        private readonly TransformXZTargetFollower _targetFollower;
        #endregion
        
        #region Methods
        public FollowingState(LitosSlapHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _handController = handController;
            _targetFollower = new TransformXZTargetFollower(_handController.transform, _handController.Velocity);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _targetFollower.SetTargetPosition(_handController.Player.position);
            
            if (_handController.PlayerIsInRange)
            {
                stateMachine.SetState(new SlapAttackState(_handController, stateMachine, anim));
            }
        }
        #endregion
    }
}