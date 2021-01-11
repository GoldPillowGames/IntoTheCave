using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.SinCabeza
{
    public class AttackState : AnimatedState
    {
        #region Variables
        private readonly SinCabezaController _enemyController;
        private readonly MethodDelayer _nextStateDelayer;
        private readonly RotateTowardsTarget _rotateTowards;
        #endregion
        
        #region Methods
        public AttackState(SinCabezaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;

            _rotateTowards = new RotateTowardsTarget(_enemyController.transform, _enemyController.Player, _enemyController.RotationSpeed);
            _nextStateDelayer = new MethodDelayer(ThrowNewOrb);
            
            animationBoolParameterSelector.Add("IsAttacking");
        }

        public override void Enter()
        {
            base.Enter();
            
            _nextStateDelayer.SetNewDelay(_enemyController.TimeForNextOrb);
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

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);
            _nextStateDelayer.Update(deltaTime);
        }

        private void ThrowNewOrb()
        { 
            var currentState = anim.GetCurrentAnimatorStateInfo(0);
            var stateName = currentState.fullPathHash;
            
            anim.Play(stateName,0, 0.0f);
            
            _nextStateDelayer.SetNewDelay(_enemyController.TimeForNextOrb);
        }
        #endregion
    }
}