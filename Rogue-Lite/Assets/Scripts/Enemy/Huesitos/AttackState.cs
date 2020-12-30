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
        #endregion
        
        #region Methods
        public AttackState(HuesitosController enemyController, FiniteStateMachine stateMachine, Animator anim, int animAttackComboIndex = 1) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsAttacking" + animAttackComboIndex);
        }

        public override void Enter()
        {
            base.Enter();
            
            _enemyController.OnComboHitEnding = GoToNextComboState;
        }

        private void GoToNextComboState(int animAttackComboIndex)
        {
            if (_enemyController.CanAttack)
            {
                stateMachine.SetState(new AttackState(_enemyController, stateMachine, anim, animAttackComboIndex));
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}