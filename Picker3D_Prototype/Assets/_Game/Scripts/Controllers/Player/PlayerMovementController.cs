using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using UnityEngine;

namespace Assets._Game.Scripts.Actors
{
    public class PlayerMovementController : Actor<PlayerManager>
    {
        [SerializeField] private Transform playerObj;

        private float xMouseDelta;
     
        protected override void ConfigureSubscriptions(bool status)
        {
            GamePlayInputSignals.Instance?.onMouseDragged.Subscribe(OnMouseDragged, status);
        }

        private void OnMouseDragged(float x)
        {
            xMouseDelta = x;
        }

        private void Update()
        {
            playerObj.position = new(playerObj.position.x + xMouseDelta, 0);
        }

    }
}