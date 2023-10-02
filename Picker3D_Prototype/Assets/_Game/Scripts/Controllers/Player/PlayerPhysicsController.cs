using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Game.Scripts.Actors
{
    public class PlayerPhysicsController : Actor<PlayerManager>
    {
        [SerializeField] private float _pickerOverlapRadius;
        [SerializeField] private Transform _pickerOverlapPoint;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance.onReachedToPool?.Subscribe(ThrowBalls, status);
        }

        private void ThrowBalls()
        {
            Collider[] colliders = Physics.OverlapSphere(_pickerOverlapPoint.position, _pickerOverlapRadius, GameSettings.Instance.collectableBallLayer.value);

            foreach (var col in colliders)
            {
                if (!col.TryGetComponent<Rigidbody>(out var rb)) 
                    continue;

                rb.AddForce(GameSettings.Instance.ballForceValue, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_pickerOverlapPoint.position, _pickerOverlapRadius);
        }
    }
}