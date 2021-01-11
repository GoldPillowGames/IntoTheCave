using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.SinCabeza {
    public class FollowingState : AnimatedState
    {
        #region Variables
        private readonly SinCabezaController _enemyController;
        private readonly ITargetFollower _targetFollower;
        #endregion
        
        #region Methods
        public FollowingState(SinCabezaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add(new string[] {"IsFollowing"});
            _enemyController = enemyController;
            _targetFollower = new NavMeshTargetFollower(_enemyController.Agent);
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.Agent.isStopped = false;
            _enemyController.Agent.speed = _enemyController.Velocity;
            
            _targetFollower.SetTarget(_enemyController.Player);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _targetFollower.Update(deltaTime);
            
            if (_enemyController.CanAttack)
            {
                stateMachine.SetState(new AttackState(_enemyController, stateMachine, anim));
            }
        }

        public override void Exit()
        {
            base.Exit();

            _enemyController.Agent.velocity = Vector3.zero;
            _enemyController.Agent.isStopped = true;
        }
        #endregion
    }
}