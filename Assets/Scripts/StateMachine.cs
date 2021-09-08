
namespace Assets.Scripts
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        public void Init(IState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        public void ChangeState(IState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}
