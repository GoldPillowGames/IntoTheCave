using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos
{
    public class DeathState : AnimatedState
    {
        #region Variables
        private readonly LitosController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(LitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsDying");
            _enemyController = enemyController;
        }
        #endregion
    }
}