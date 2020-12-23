using GoldPillowGames.Patterns;
using GoldPillowGames.Core;
using UnityEngine;

namespace GoldPillowGames.Enemy.ZombieKnight
{
    public class AttackState : EnemyState
    {
        #region Variables
        private readonly ZombieKnightController _enemyController;
        private readonly MethodDelayer _transitionDelayer;
        #endregion
        
        #region Methods
        public AttackState(ZombieKnightController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            _transitionDelayer = new MethodDelayer(GoToNextState);
        }

        public override void Enter()
        {
            base.Enter();
            _transitionDelayer.SetNewDelay(_enemyController.CurrentAnimationLength);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _transitionDelayer.Update(deltaTime);
        }

        private void GoToNextState()
        {
            if (_enemyController.CanAttack && !_enemyController.IsThereAnObstacleInAttackRange())
            {
                _transitionDelayer.SetNewDelay(_enemyController.CurrentAnimationLength);
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}