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
            animatorBoolParameterName = "IsAttacking";
            _enemyController = enemyController;
            _transitionDelayer = new MethodDelayer(GoToNextState);
        }

        public override void Enter()
        {
            base.Enter();
            _transitionDelayer.SetNewDelay(_enemyController.CurrentAnimationLength); // Todavía no se ha cambiado de estado en el Animator porque el cambio de booleano se ha hecho en el mismo frame. Hacer que esto se ejecute en el siguiente frame o en vez de usar booleanos reproducir las animaciones directamente (esto no permitiría exit times).
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
                Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                anim.Play("Attack 1", -1, 0);
            }
            else
            {
                stateMachine.SetState(new FollowingState(_enemyController, stateMachine, anim));
            }
        }
        #endregion
    }
}