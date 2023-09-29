using Assets._Game.Scripts.Abstracts;
using Assets._Game.Scripts.Data;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Signals
{
    public class CoreSignals : SignalGroup<CoreSignals>
    {
        public UnityEvent onGameStarted;
        public UnityEvent onLevelStarted;
        public UnityEvent onLevelCompleted;
        public UnityEvent onLevelFailed;

        public UnityEvent<PlayerData> onPlayerDataLoaded;

        public UnityEvent onReachedToPool;
        public UnityEvent onPoolClosed;

        public UnityEvent onReachedToFinishArea;

    }
}
