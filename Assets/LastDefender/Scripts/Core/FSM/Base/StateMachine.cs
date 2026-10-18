namespace Assets.LastDefender.Scripts.Core.FSM.Base
{
    public class StateMachine
    {
        public BaseState CurrentState { get; private set; }

        public void Init(BaseState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
