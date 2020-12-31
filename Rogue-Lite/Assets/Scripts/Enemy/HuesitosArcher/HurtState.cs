using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class HurtState : EnemyState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyController;
        #endregion
        
        #region Methods
        public HurtState(HuesitosArcherController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("isReceivingDamage");
            _enemyController = enemyController;
        }

        public override void Enter()
        {
            base.Enter();
            
            _enemyController.GoToNextStateCallback = GoToNextState;
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