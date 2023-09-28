using Assets._Game.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Signals
{
    public class CoreSignals : MonoSingleton<CoreSignals>
    {
        public UnityEvent onGameStarted;
        public UnityEvent onLevelStarted;
        public UnityEvent onLevelCompleted;

        public UnityEvent<PlayerData> onPlayerDataLoaded;

    }
}
