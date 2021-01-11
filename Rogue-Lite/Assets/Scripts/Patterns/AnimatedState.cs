using GoldPillowGames.Core;
using UnityEngine;

namespace GoldPillowGames.Patterns
{
    public class AnimatedState : State
    {
        #region Variables
        protected readonly RandomStringSelector animationBoolParameterSelector;
        protected readonly Animator anim;
        private string _animatorBoolParameterName;
        #endregion
        
        #region Methods
        protected AnimatedState(FiniteStateMachine stateMachine, Animator anim) : base(stateMachine)
        {
            this.anim = anim;
            animationBoolParameterSelector = new RandomStringSelector();
        }

        public override void Enter()
        {
            base.Enter();

            _animatorBoolParameterName = animationBoolParameterSelector.ChooseRandom();

            if (_animatorBoolParameterName == default)
            {
                return;
            }
            
            anim.SetBool(_animatorBoolParameterName, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            if (_animatorBoolParameterName == default)
            {
                return;
            }
            
            anim.SetBool(_animatorBoolParameterName, false);
        }
        #endregion
    }
}