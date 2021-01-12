using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.SinCabeza
{
    public class DeathState : AnimatedState
    {
        #region Variables
        private readonly SinCabezaController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(SinCabezaController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add(new string[] {"IsDying1", "IsDying2"});
        }
        #endregion
    }
}