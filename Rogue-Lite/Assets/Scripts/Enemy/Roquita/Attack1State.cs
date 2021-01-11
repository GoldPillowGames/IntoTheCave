using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class Attack1State : AnimatedState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        #endregion
        
        #region Methods
        public Attack1State(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsAttacking1");
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
            if (_enemyController.CanAttack)
            {
                if (Random.value >= 0.5f)
                {
                    _enemyController.ResetState(new Attack1State(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new Attack2State(_enemyController, stateMachine, anim));
                }
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}