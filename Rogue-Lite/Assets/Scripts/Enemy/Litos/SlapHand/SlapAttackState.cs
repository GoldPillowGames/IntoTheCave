using GoldPillowGames.Enemy.Roquita;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.SlapHand
{
    public class SlapAttackState : AnimatedState
    {
        #region Variables
        private readonly LitosSlapHandController _handController;
        #endregion
        
        #region Methods
        public SlapAttackState(LitosSlapHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsAttacking");
            _handController = handController;
        }

        public override void Enter()
        {
            base.Enter();

            _handController.GoToNextStateCallback = GoToNextState;
            _handController.ChildAnim.SetBool("IsAttacking", true);
        }

        private void GoToNextState()
        {
            stateMachine.SetState(new RecoveringState(_handController, stateMachine, anim));
        }
        #endregion
    }
}