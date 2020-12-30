using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher {
    public class FollowingState : EnemyState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyArcherController;
        private readonly ITargetFollower _targetFollower;
        #endregion
        
        #region Methods
        public FollowingState(HuesitosArcherController enemyArcherController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add(new string[] {"IsFollowing1", "IsFollowing2"});
            _enemyArcherController = enemyArcherController;
            _targetFollower = new NavMeshTargetFollower(_enemyArcherController.Agent);
        }

        public override void Enter()
        {
            base.Enter();

            _enemyArcherController.Agent.isStopped = false;
            _enemyArcherController.Agent.speed = _enemyArcherController.Velocity;
            
            _targetFollower.SetTarget(_enemyArcherController.Player);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _targetFollower.Update(deltaTime);
            
            if (_enemyArcherController.CanAttack)
            {
                stateMachine.SetState(new BowPreparingState(_enemyArcherController, stateMachine, anim));
            }
        }

        public override void Exit()
        {
            base.Exit();

            _enemyArcherController.Agent.velocity = Vector3.zero;
            _enemyArcherController.Agent.isStopped = true;
        }
        #endregion
    }
}