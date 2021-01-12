using GoldPillowGames.Enemy.Litos.SlapHand;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.LaserHand
{
    public class IdleState : AnimatedState
    {
        #region Variables
        private readonly LitosLaserHandController _handController;
        #endregion
        
        #region Methods
        public IdleState(LitosLaserHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _handController = handController;
            
            animationBoolParameterSelector.Add("IsIdle");
        }

        #endregion
    }
}