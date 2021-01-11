using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class BowPreparingState : AnimatedState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyController;
        private readonly RotateTowardsTarget _rotateTowards;
        private readonly MethodDelayer _fireDelayer;
        #endregion
        
        #region Methods
        public BowPreparingState(HuesitosArcherController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;

            _rotateTowards = new RotateTowardsTarget(_enemyController.transform, _enemyController.Player, _enemyController.RotationSpeed);
            _fireDelayer = new MethodDelayer(Fire);
            
            animationBoolParameterSelector.Add("IsPreparing");
        }

        public override void Enter()
        {
            base.Enter();
            
            _fireDelayer.SetNewDelay(_enemyController.TimeToFire);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            _rotateTowards.Update(deltaTime);
            
            if (!_enemyController.CanAttack)
            {
                _enemyController.HideVisualArrow();
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);
            
            _fireDelayer.Update(deltaTime);
        }

        private void Fire()
        {
            stateMachine.SetState(new BowFiringState(_enemyController, stateMachine, anim));
        }
        #endregion
    }
}