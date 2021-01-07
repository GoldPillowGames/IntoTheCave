using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class Attack2State : EnemyState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        #endregion
        
        #region Methods
        public Attack2State(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsAttacking2");
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
                    _enemyController.ResetState(new Attack2State(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new Attack1State(_enemyController, stateMachine, anim));
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