using Assets._Game.Scripts.GameStates.InputManagement;
using Assets._Game.Scripts.Signals;
using Assets._Game.Scripts.StateMachine;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Game.Scripts.Managers
{
    public class InputManager : Manager<InputManager>
    {
        private StateMachine<IStateForInput> _stateMachineForInputs;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onGameStarted.Subscribe(OnGameStarted, status);
            CoreSignals.Instance?.onLevelStarted.Subscribe(OnLevelStarted, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);
        }

        private void Awake()
        {
            _stateMachineForInputs = new StateMachine<IStateForInput>();
        }

        private void Update()
        {
            _stateMachineForInputs.OnUpdate();
        }

        private void OnGameStarted()
        {
            _stateMachineForInputs.ChangeState(new ReadyToPlayInputState(_stateMachineForInputs));
        }

        private void OnLevelStarted()
        {
            _stateMachineForInputs.ChangeState(new GamePlayInputState(_stateMachineForInputs));
        }

        private void OnLevelCompleted(bool isSuccess)
        {
            _stateMachineForInputs.ChangeState(null);
        }
    }
}
