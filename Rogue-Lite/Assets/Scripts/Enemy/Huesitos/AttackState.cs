using System.Collections;
using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Huesitos
{
    public class AttackState : EnemyState
    {
        #region Variables
        private readonly HuesitosController _enemyController;
        private readonly MethodDelayer _stateChangeDelayer;
        #endregion
        
        #region Methods
        public AttackState(HuesitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add(_enemyController.AnimationAttackComboSelector.GetNextAttackComboBoolParameter());
            
            _stateChangeDelayer = new MethodDelayer(GoToNextState);
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.StartCoroutine(SetStateChangeDelay());
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _stateChangeDelayer.Update(deltaTime);
        }

        private void GoToNextState()
        {
            if (_enemyController.CanAttack && _enemyController.CanSeePlayer() && !_enemyController.IsThereAnObstacleInAttackRange())
            {
                stateMachine.SetState(new AttackState(_enemyController, stateMachine, anim));
            }
            else
            {
                _enemyController.AnimationAttackComboSelector.ResetCombo();
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }

        private IEnumerator SetStateChangeDelay()
        {
            // Wait a frame to get the correct animation length once it has been updated in the Animator.
            yield return 0;
            
            _stateChangeDelayer.SetNewDelay(_enemyController.CurrentAnimationLength);
        }
        #endregion
    }
}