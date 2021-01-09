using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class AttackMelee21State : EnemyState
    {
        #region Variables
        private readonly PinchitosController _enemyController;
        #endregion
        
        #region Methods
        public AttackMelee21State(PinchitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsAttackingMelee2-1");
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.SetMelee21Damage();
            
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
                stateMachine.SetState(new AttackMelee22State(_enemyController, stateMachine, anim));
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}