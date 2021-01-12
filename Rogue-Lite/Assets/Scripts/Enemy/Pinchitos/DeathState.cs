using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Pinchitos
{
    public class DeathState : AnimatedState
    {
        #region Variables
        private readonly PinchitosController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(PinchitosController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add("IsDying");
            _enemyController = enemyController;
        }
        #endregion
    }
}