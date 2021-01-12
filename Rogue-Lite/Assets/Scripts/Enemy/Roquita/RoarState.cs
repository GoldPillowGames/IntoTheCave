using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class RoarState : AnimatedState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        #endregion
        
        #region Methods
        public RoarState(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsRoaring");
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
                    stateMachine.SetState(new Attack1State(_enemyController, stateMachine, anim));
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