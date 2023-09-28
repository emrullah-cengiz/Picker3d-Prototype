using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Controllers.PanelControllers
{
    public class LevelStartPanelController : Actor<GameManager>
    {
        [SerializeField] private Button panelButton;

        protected override void ConfigureSubscriptions(bool status)
        {
            panelButton.onClick.Subscribe(TouchedToScreen, status);
        }

        private void TouchedToScreen()
        {
            CoreSignals.Instance.onLevelStarted?.Invoke();
        }
    }
}
