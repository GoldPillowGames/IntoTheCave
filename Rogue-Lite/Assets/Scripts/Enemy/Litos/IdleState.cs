using GoldPillowGames.Core;
using GoldPillowGames.Enemy.Pinchitos;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos {
    public class IdleState : EnemyState
    {
        #region Variables
        private readonly LitosController _enemyController;
        #endregion
        
        #region Methods
        public IdleState(LitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _enemyController = enemyController;
            
            animationBoolParameterSelector.Add("IsIdle");
        }
        #endregion
    }
}