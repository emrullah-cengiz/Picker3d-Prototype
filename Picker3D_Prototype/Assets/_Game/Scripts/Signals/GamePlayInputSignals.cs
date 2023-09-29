using Assets._Game.Scripts.Abstracts;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Signals
{
    public class GamePlayInputSignals : SignalGroup<GamePlayInputSignals>
    {
        public UnityEvent onMouseDown;
        public UnityEvent onMouseUp;

        public UnityEvent<float> onMouseDragged;
    }
}
