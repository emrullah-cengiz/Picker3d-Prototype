using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Controllers
{
    public class GamePlayPanelController : Actor<GameManager>
    {
        [SerializeField] private TMP_Text _currentLevelIndicatorText;
        [SerializeField] private TMP_Text _nextLevelIndicatorText;
        [SerializeField] private Transform _poolIndicatorsParent;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance.onLevelSpawned.Subscribe(OnLevelSpawned, status);
        }

        private void OnLevelSpawned(LevelData levelData)
        {
            CoreSignals.Instance.onRetryButtonClick?.Invoke();
        }
    }
}
