using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Enums;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Game.Scripts.Actors
{
    public class UIPanelsController : Actor<UIManager>
    {
        [SerializeField] private Transform _panelsParent;

        private Dictionary<UIPanel, GameObject> _openPanels;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);
            CoreSignals.Instance?.onLevelStarted.Subscribe(OnLevelStarted, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);

            UISignals.Instance?.onPanelOpened.Subscribe(OpenPanel, status);
            UISignals.Instance?.onPanelClosed.Subscribe(ClosePanel, status);
        }

        private void Awake() => _openPanels = new();

        private void OnLevelStarted() => UISignals.Instance?.onPanelOpened?.Invoke(UIPanel.GamePlay, true);
        private void OnLevelSpawned(LevelData levelData) => UISignals.Instance?.onPanelOpened?.Invoke(UIPanel.LevelStart, true);
        private void OnLevelCompleted(LevelCompletionInfo levelCompletionData) => 
                            UISignals.Instance?.onPanelOpened?.Invoke(levelCompletionData.IsSuccess ? UIPanel.Success : UIPanel.Fail, true);

        private void OpenPanel(UIPanel uiPanel, bool closeCurrent = true)
        {
            if (_openPanels.ContainsKey(uiPanel))
                return;

            if (closeCurrent)
                while(_openPanels.Count > 0)
                    UISignals.Instance?.onPanelClosed?.Invoke(_openPanels.ElementAt(0).Key);

            var gameSettings = GameSettings.Instance;

            string panelPrefabPath = $"{gameSettings.uiPanelsResourcePath}/" +
                                     $"{string.Format(gameSettings.uiPanelPrefabNameFormat, uiPanel.ToString())}";

            _openPanels.Add(uiPanel, Instantiate(Resources.Load<GameObject>(panelPrefabPath), _panelsParent));
        }

        private void ClosePanel(UIPanel uiPanel)
        {
            if (!_openPanels.TryGetValue(uiPanel, out var panel))
                return;

            Destroy(panel);

            _openPanels.Remove(uiPanel);
        }
    }
}