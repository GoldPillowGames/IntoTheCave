using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Roquita;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.SlapHand
{
    public class RecoveringState : AnimatedState
    {
        #region Variables
        private readonly LitosSlapHandController _handController;
        private readonly TransformXZTargetFollower _targetFollower;
        #endregion
        
        #region Methods
        public RecoveringState(LitosSlapHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _handController = handController;
            _targetFollower = new TransformXZTargetFollower(_handController.transform, _handController.Velocity);
            
            animationBoolParameterSelector.Add("IsIdle");
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _handController.ChildAnim.SetBool("IsAttacking", false);
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _targetFollower.SetTargetPosition(_handController.InitialPosition);
            
            if (_handController.IsClosedToInitPosition)
            {
                _handController.FinishAttack();
                stateMachine.SetState(new IdleState(_handController, stateMachine, anim));
            }
        }
        #endregion
    }
}