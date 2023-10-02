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
            CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);

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

        private void OnLevelSpawned(LevelData arg0)
        {
            SetMovability(false);

            playerRb.position = Vector3.zero;
        }

        private void OnLevelStarted() => SetMovability(true);
        private void OnLevelCompleted(uint levelNum, bool isSuccess) => SetMovability(false);
        private void OnReachedToPool() => SetMovability(false);
        private void OnPoolClosed() => SetMovability(true);

        private void OnMouseDragged(float x) => xMouseDelta = x;

        private void SetMovability(bool status) => isReadyForMoveFwd = status;

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