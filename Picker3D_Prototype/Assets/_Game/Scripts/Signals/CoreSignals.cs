using Assets._Game.Scripts.Abstracts;
using Assets._Game.Scripts.Data;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Signals
{
    public class CoreSignals : SignalGroup<CoreSignals>
    {
        public UnityEvent onGameStarted;

        public UnityEvent onLevelStarted;
        public UnityEvent<LevelCompletionInfo> onLevelCompleted;

        public UnityEvent<LevelData> onLevelSpawned;

        public UnityEvent onNextLevelButtonClick;
        public UnityEvent onRetryButtonClick;

        public UnityEvent onPlayerDataLoaded;

        public UnityEvent onReachedToPool;
        public UnityEvent<bool> onPoolClosed;

        public UnityEvent onReachedToFinishArea;

        public UnityEvent<bool> onPoolInteractedWithBall;


    }
}
