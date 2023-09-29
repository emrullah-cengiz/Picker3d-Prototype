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
            ///TODO: Kaldır
            _movementSettings = GameSettings.Instance.movementSettings;


            if (Input.GetMouseButton(0))
            {
                _mouseDelta = _firstMousePosition - Input.mousePosition;
                //_xMovementData = _mouseDelta.x * _movementSettings.XMovementSpeed;

                //_xMovementData = Mathf.SmoothDamp(_xMovementData, 0f, ref _currentVelocity, _movementSettings.XSmoothTime);

                GamePlayInputSignals.Instance?.onMouseDragged?.Invoke(_mouseDelta.x);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currentVelocity = 0;
                _mouseDelta = Vector3.zero;
            }
        }
    }
}
