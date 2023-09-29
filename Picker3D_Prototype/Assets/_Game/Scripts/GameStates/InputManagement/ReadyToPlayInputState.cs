using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using Assets._Game.Scripts.StateMachine;
using UnityEngine;

namespace Assets._Game.Scripts.GameStates.InputManagement
{
    public class ReadyToPlayInputState : State<IStateForInput>, IStateForInput
    {
        public ReadyToPlayInputState(StateMachine<IStateForInput> stateMachine) : base(stateMachine)
        {
        }

        public override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CoreSignals.Instance?.onLevelStarted?.Invoke();

                GamePlayInputSignals.Instance?.onMouseDown?.Invoke();
            }
        }
    }

}
