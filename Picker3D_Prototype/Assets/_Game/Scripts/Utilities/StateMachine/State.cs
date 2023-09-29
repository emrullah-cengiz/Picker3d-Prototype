namespace Assets._Game.Scripts.StateMachine
{
    public abstract class State<T> : IState where T : IState
    {
        protected StateMachine<T> StateMachine;

        public State(StateMachine<T> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnStart() { }

        public virtual void OnUpdate() { }
    }
}
