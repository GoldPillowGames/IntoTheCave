using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class BowPreparingState : EnemyState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyController;
        private readonly RotateTowardsTarget _rotateTowards;
        #endregion
        
        #region Methods
        public BowPreparingState(HuesitosArcherController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;

            _rotateTowards = new RotateTowardsTarget(_enemyController.transform, _enemyController.Player, _enemyController.RotationSpeed);
            
            animationBoolParameterSelector.Add("IsPreparing");
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.GoToNextStateCallback = GoToNextState;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _rotateTowards.Update(deltaTime);
            
            if (!_enemyController.CanAttack)
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }

        private void GoToNextState()
        {
            if (_enemyController.CanAttack)
            {
                stateMachine.SetState(new BowFiringState(_enemyController, stateMachine, anim));
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}