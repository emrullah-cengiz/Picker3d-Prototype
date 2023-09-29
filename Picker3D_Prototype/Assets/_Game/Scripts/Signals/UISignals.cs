using Assets._Game.Scripts.Abstracts;
using Assets._Game.Scripts.Enums;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Signals
{
    public class UISignals : SignalGroup<UISignals>
    {
        public UnityEvent<UIPanel> onPanelOpened;
        public UnityEvent<UIPanel> onPanelClosed;

        //public UnityEvent onLevelMenuOpened;

        //private void OnEnable()
        //{
        //    onPanelOpened.Subscribe(OnPanelOpened, true);
        //}

        //private void OnPanelOpened(UIPanel uiPanel)
        //{
        //    var relatedEvent = uiPanel switch
        //    {
        //        UIPanel.LevelsMenu => onLevelMenuOpened,
        //        UIPanel.
        //        _ => null,
        //    };

        //    relatedEvent?.Invoke();
        //}
    }
}
