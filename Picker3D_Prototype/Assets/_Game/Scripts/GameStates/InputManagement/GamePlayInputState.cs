using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using Assets._Game.Scripts.StateMachine;
using UnityEngine;

namespace Assets._Game.Scripts.GameStates.InputManagement
{
    public class GamePlayInputState : State<IStateForInput>, IStateForInput
    {
        private MovementSettings _movementSettings;

        private bool _isTouching;

        private Vector3 _firstMousePosition;
        private Vector3 _mouseDelta;
        private float _xMovementData;

        private float _currentVelocity;

        public GamePlayInputState(StateMachine<IStateForInput> stateMachine) : base(stateMachine)
        {
            _movementSettings = GameSettings.Instance.movementSettings;
        }

        public override void OnStart()
        {
            //Touching is started on previous state
            _isTouching = true;

            _firstMousePosition = Input.mousePosition;
        }

        public override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                _firstMousePosition = Input.mousePosition;
            }

            if (_isTouching)
            {
                _mouseDelta = Input.mousePosition - _firstMousePosition;

                Debug.Log(_mouseDelta);
               
                GamePlayInputSignals.Instance?.onMouseDragged?.Invoke(_mouseDelta.x);

                _firstMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                _currentVelocity = 0;
                _mouseDelta = Vector3.zero;

                GamePlayInputSignals.Instance?.onMouseDragged?.Invoke(0);
            }
        }
    }
}
