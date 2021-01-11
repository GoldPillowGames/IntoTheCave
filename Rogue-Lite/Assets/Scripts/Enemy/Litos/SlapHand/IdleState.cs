using GoldPillowGames.Enemy.Roquita;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.Litos.SlapHand
{
    public class IdleState : AnimatedState
    {
        #region Variables
        private readonly LitosSlapHandController _handController;
        #endregion
        
        #region Methods
        public IdleState(LitosSlapHandController handController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            _handController = handController;
        }

        #endregion
    }
}