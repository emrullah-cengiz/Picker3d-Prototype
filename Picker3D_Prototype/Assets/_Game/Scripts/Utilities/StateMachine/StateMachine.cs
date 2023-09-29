using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Game.Scripts.StateMachine
{
    public class StateMachine<T> where T : IState
    {
        private T? currentState;

        public void ChangeState(T newState)
        {
            if (currentState == null || !currentState.Equals(newState))
            {
                currentState = newState;

                currentState?.OnStart();
            }
        }

        public T GetCurrentState()
        {
            return currentState;
        }

        public void OnUpdate()
        {
            currentState?.OnUpdate();
        }
    }
}
