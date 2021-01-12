using System.Collections;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class BowFiringState : AnimatedState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyController;
        #endregion
        
        #region Methods
        public BowFiringState(HuesitosArcherController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsFiring");
        }

        public override void Enter()
        {
            base.Enter();
            
            _enemyController.GoToNextStateCallback = GoToNextState;
        }

        public override void Exit()
        {
            base.Exit();
            
            _enemyController.HideVisualArrow();
            _enemyController.GoToNextStateCallback = null;
        }

        private void GoToNextState()
        {
            if (_enemyController.CanAttack)
            {
                stateMachine.SetState(new BowPreparingState(_enemyController, stateMachine, anim));
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}