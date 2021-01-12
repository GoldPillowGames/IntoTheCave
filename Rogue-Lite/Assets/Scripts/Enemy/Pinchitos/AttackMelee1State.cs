using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class AttackMelee1State : AnimatedState
    {
        #region Variables
        private readonly PinchitosController _enemyController;
        #endregion
        
        #region Methods
        public AttackMelee1State(PinchitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsAttackingMelee1");
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.SetMelee1Damage();
            
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
                var randomValue = Random.value;
                if (randomValue <= 0.4f)
                {
                    stateMachine.SetState(new AttackMelee21State(_enemyController, stateMachine, anim));
                }
                else if (randomValue <= 0.8f)
                {
                    _enemyController.ResetState(new AttackMelee1State(_enemyController, stateMachine, anim));
                }
                else if (_enemyController.CanThrow)
                {
                    stateMachine.SetState(new AttackCannonState(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new AttackMelee1State(_enemyController, stateMachine, anim));
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