using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class JumpAttackState : AnimatedState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        #endregion
        
        #region Methods
        public JumpAttackState(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsJumping");
        }

        public override void Enter()
        {
            base.Enter();
            
            _enemyController.GoToNextStateCallback = GoToNextState;
        }

        public override void Exit()
        {
            base.Exit();
            
            _enemyController.GoToNextStateCallback = null;
        }

        private void GoToNextState()
        {
            stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
        }
        #endregion
    }
}