﻿using Assets._Game.Scripts.Actors;
using Assets._Game.Scripts.Signals;
using UnityEngine;

namespace Assets._Game.Scripts.Managers
{
    public class GameManager : Manager<GameManager>
    {

        private void Awake()
        {
            CoreSignals.Instance.onGameStarted?.Invoke();
        }
    }
}
