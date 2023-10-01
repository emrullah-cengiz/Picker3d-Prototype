using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using UnityEngine;

namespace Assets._Game.Scripts.Actors
{
    public class PlayerMovementController : Actor<PlayerManager>
    {
        [SerializeField] private Rigidbody playerRb;

        private float xMouseDelta;
        private bool isReadyForMoveFwd;

        MovementSettings _movementSettings;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onLevelStarted.Subscribe(OnLevelStarted, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);

            CoreSignals.Instance?.onReachedToPool.Subscribe(OnReachedToPool, status);
            CoreSignals.Instance?.onPoolClosed.Subscribe(OnPoolClosed, status);

            GamePlayInputSignals.Instance?.onMouseDragged.Subscribe(OnMouseDragged, status);
        }

        private void Start()
        {
            _movementSettings = GameSettings.Instance.movementSettings;
        }

        private void OnLevelStarted() => isReadyForMoveFwd = true;
        private void OnLevelCompleted(bool isSuccess) => isReadyForMoveFwd = false;

        private void OnReachedToPool() => isReadyForMoveFwd = false;
        private void OnPoolClosed() => isReadyForMoveFwd = true;

        private void OnMouseDragged(float x) => xMouseDelta = x;

        private void FixedUpdate()
        {
            Vector3 newPosition = playerRb.transform.position + Vector3.right * (xMouseDelta * _movementSettings.XMovementSpeed * Time.fixedDeltaTime);

            newPosition.x = Mathf.Clamp(newPosition.x, -_movementSettings.XClamp, _movementSettings.XClamp);

            if (isReadyForMoveFwd)
                newPosition.z += _movementSettings.ZMovementSpeed * Time.fixedDeltaTime;

            playerRb.MovePosition(newPosition);
        }

    }
}