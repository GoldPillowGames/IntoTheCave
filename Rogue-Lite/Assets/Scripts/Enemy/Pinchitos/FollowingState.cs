using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos {
    public class FollowingState : AnimatedState
    {
        #region Variables
        private readonly PinchitosController _enemyController;
        private readonly ITargetFollower _targetFollower;
        private readonly MethodDelayer _spikeBallThrowDelayer;
        private bool _isFirstFrame = true;
        private bool _canAlreadyThrowSpikeBall;
        #endregion
        
        #region Methods
        public FollowingState(PinchitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsFollowing");
            _enemyController = enemyController;
            _targetFollower = new NavMeshTargetFollower(_enemyController.Agent);
            _spikeBallThrowDelayer = new MethodDelayer(AttackCannon);
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.Agent.isStopped = false;
            _enemyController.Agent.speed = _enemyController.Velocity;
            
            _targetFollower.SetTarget(_enemyController.Player);
            _spikeBallThrowDelayer.SetNewDelay(_enemyController.TimeToThrowSpikeBall);
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

            if (_enemyController.CanThrow && _canAlreadyThrowSpikeBall && !_enemyController.IsThereAnObstacleInFrontOfPlayer())
            {
                stateMachine.SetState(new AttackCannonState(_enemyController, stateMachine, anim));
            }
            else if (_enemyController.CanAttack)
            {
                float randomValue = Random.value;
                if (randomValue < 0.4f)
                {
                    stateMachine.SetState(new AttackMelee1State(_enemyController, stateMachine, anim));
                }
                else if (randomValue < 0.8f)
                {
                    stateMachine.SetState(new AttackMelee21State(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new AttackCannonState(_enemyController, stateMachine, anim));
                }
            }
        }
        
        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);
            
            _spikeBallThrowDelayer.Update(deltaTime);
        }
        
        public override void Exit()
        {
            base.Exit();

            _enemyController.Agent.velocity = Vector3.zero;
            _enemyController.Agent.isStopped = true;
        }

        private void AttackCannon()
        {
            if (_enemyController.CanThrow && !_enemyController.IsThereAnObstacleInFrontOfPlayer())
            {
                stateMachine.SetState(new AttackCannonState(_enemyController, stateMachine, anim));
            }
            else
            {
                _canAlreadyThrowSpikeBall = true;
            }
        }
        #endregion
    }
}