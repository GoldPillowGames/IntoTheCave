using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita {
    public class FollowingState : EnemyState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        private readonly ITargetFollower _targetFollower;
        private readonly MethodDelayer _jumpDelayer;
        private bool _isFirstFrame = true;
        #endregion
        
        #region Methods
        public FollowingState(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsFollowing");
            _enemyController = enemyController;
            _targetFollower = new NavMeshTargetFollower(_enemyController.Agent);
            _jumpDelayer = new MethodDelayer(Jump);
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.Agent.isStopped = false;
            _enemyController.Agent.speed = _enemyController.Velocity;
            
            _targetFollower.SetTarget(_enemyController.Player);
            _jumpDelayer.SetNewDelay(_enemyController.TimeToJump);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_isFirstFrame)
            {
                _isFirstFrame = false;
                return;
            }
            
            _targetFollower.Update(deltaTime);
            
            if (_enemyController.CanAttack)
            {
                if (Random.value < 0.5f)
                {
                    stateMachine.SetState(new Attack1State(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new Attack2State(_enemyController, stateMachine, anim));
                }
            }
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);
            
            _jumpDelayer.Update(deltaTime);
        }

        private void Jump()
        {
            stateMachine.SetState(new JumpAttackState(_enemyController, stateMachine, anim));
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