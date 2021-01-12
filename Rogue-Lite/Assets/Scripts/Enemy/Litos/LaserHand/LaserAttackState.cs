using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Litos.SlapHand;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.LaserHand
{
    public class LaserAttackState : AnimatedState
    {
        #region Variables
        private readonly LitosLaserHandController _handController;
        private readonly TransformXZTargetFollower _targetFollower;
        #endregion
        
        #region Methods
        public LaserAttackState(LitosLaserHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsAttacking");
            _handController = handController;
            _targetFollower = new TransformXZTargetFollower(_handController.transform, _handController.VelocityWhenLaserOn);
        }

        public override void Enter()
        {
            base.Enter();

            _handController.ChildAnim.SetBool("IsAttacking", true);

            _handController.LaserEndCallback = LaserEnd;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_handController.IsLaserOn)
            {
                _targetFollower.SetTargetPosition(_handController.Player.position);
            }
        }

        public override void Exit()
        {
            base.Exit();

            _handController.LaserEndCallback = null;
        }

        private void LaserEnd()
        {
            stateMachine.SetState(new RecoveringState(_handController, stateMachine, anim));
        }
        #endregion
    }
}