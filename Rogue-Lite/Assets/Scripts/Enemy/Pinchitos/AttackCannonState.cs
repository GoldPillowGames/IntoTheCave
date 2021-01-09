using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class AttackCannonState : EnemyState
    {
        #region Variables
        private readonly PinchitosController _enemyController;
        private readonly RotateTowardsTarget _rotateTowards;
        #endregion
        
        #region Methods
        public AttackCannonState(PinchitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            _rotateTowards = new RotateTowardsTarget(_enemyController.transform, _enemyController.Player, _enemyController.RotationSpeed);
            
            animationBoolParameterSelector.Add("IsAttackingCannon");
        }

        public override void Enter()
        {
            base.Enter();

            _enemyController.CanRotate = true;
            
            _enemyController.GoToNextStateCallback = GoToNextState;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_enemyController.CanRotate)
            {
                _rotateTowards.Update(deltaTime);    
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _enemyController.GoToNextStateCallback = null;
        }

        private void GoToNextState()
        {
            if (_enemyController.CanAttack)
            {
                var randomValue = Random.value;
                if (randomValue <= 0.5f)
                {
                    stateMachine.SetState(new AttackMelee21State(_enemyController, stateMachine, anim));
                }
                else
                {
                    stateMachine.SetState(new AttackMelee1State(_enemyController, stateMachine, anim));
                }
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}