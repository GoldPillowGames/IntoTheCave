namespace GoldPillowGames.Patterns
{
    public abstract class State
    {
        #region Variables
        protected FiniteStateMachine stateMachine;
        #endregion

        #region Methods

        protected State(FiniteStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void FixedUpdate(float deltaTime)
        {

        }

        public virtual void Exit()
        {

        }
        #endregion
    }
}