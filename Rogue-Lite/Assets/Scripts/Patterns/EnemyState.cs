using UnityEngine;

namespace GoldPillowGames.Patterns
{
    public class EnemyState : State
    {
        #region Variables
        protected string animatorBoolParameterName;
        protected readonly Animator anim;
        #endregion
        
        #region Methods
        public EnemyState(FiniteStateMachine stateMachine, Animator anim) : base(stateMachine)
        {
            this.anim = anim;
        }

        public override void Enter()
        {
            base.Enter();
            
            anim.SetBool(animatorBoolParameterName, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            anim.SetBool(animatorBoolParameterName, false);
        }
        #endregion
    }
}