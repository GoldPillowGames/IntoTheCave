namespace GoldPillowGames.Patterns
{
    public class FiniteStateMachine
    {
        #region Variables
        protected State currentState;
        #endregion

        #region Methods
        public FiniteStateMachine()
        {

        }

        public void SetInitialState(State initialState)
        {
            currentState = initialState;
            currentState.Enter();
        }

        public void SetState(State incomingState)
        {
            currentState.Exit();
            currentState = incomingState;
            currentState.Enter();
        }

        public void Update(float deltaTime)
        {
            currentState.Update(deltaTime);
        }

        public void FixedUpdate(float deltaTime)
        {
            currentState.FixedUpdate(deltaTime);
        }
        #endregion
    }
}
