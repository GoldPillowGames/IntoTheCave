using GoldPillowGames.Core;
using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy
{
    public class EnemyState : State
    {
        #region Variables
        protected readonly RandomStringSelector animationBoolParameterSelector;
        protected readonly Animator anim;
        private string _animatorBoolParameterName;
        #endregion
        
        #region Methods
        protected EnemyState(FiniteStateMachine stateMachine, Animator anim) : base(stateMachine)
        {
            this.anim = anim;
            animationBoolParameterSelector = new RandomStringSelector();
        }

        public override void Enter()
        {
            base.Enter();

            _animatorBoolParameterName = animationBoolParameterSelector.ChooseRandom();
            anim.SetBool(_animatorBoolParameterName, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            anim.SetBool(_animatorBoolParameterName, false);
        }
        #endregion
    }
}