using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class DeathState : AnimatedState
    {
        #region Variables
        private readonly RoquitaController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(RoquitaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsDying");
            _enemyController = enemyController;
        }
        #endregion
    }
}