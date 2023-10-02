using Assets._Game.Scripts.Data;
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
            CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);
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

        private void OnLevelSpawned(LevelData levelData)
        {
            _stateMachineForInputs.ChangeState(new ReadyToPlayInputState(_stateMachineForInputs));
        }

        private void OnLevelStarted()
        {
            _stateMachineForInputs.ChangeState(new GamePlayInputState(_stateMachineForInputs));
        }

        private void OnLevelCompleted(uint levelNum, bool isSuccess)
        {
            _stateMachineForInputs.ChangeState(null);
        }
    }
}
