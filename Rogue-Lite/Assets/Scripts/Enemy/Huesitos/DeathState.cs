using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Huesitos
{
    public class DeathState : EnemyState
    {
        #region Variables
        private readonly HuesitosController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(HuesitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add(new string[] {"IsDying1", "IsDying2"});
            _enemyController = enemyController;
        }
        #endregion
    }
}