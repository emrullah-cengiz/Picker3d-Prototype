using Assets._Game.Scripts.Controllers.PanelControllers;
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
        [SerializeField] private PlayerDataController _playerDataController;
        [SerializeField] private LevelController _levelController;

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _currentLevelIndicatorText;
        [SerializeField] private TMP_Text _nextLevelIndicatorText;
        [SerializeField] private Transform _poolIndicatorsParent;

        private List<Image> _poolIndicators;

        private int poolCounter;

        private void Start()
        {
            _poolIndicators = new();
            _playerDataController = FindObjectOfType<PlayerDataController>();
            _levelController = FindObjectOfType<LevelController>();

            SetupUI();
        }

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onPoolClosed.Subscribe(OnPoolClosed, status);
        }

        private void OnPoolClosed(bool isFilled)
        {
            if (!isFilled) return;

            _poolIndicators[poolCounter].color = GameSettings.Instance.filledPoolIndicatorColor;
            poolCounter++;
        }

        private void SetupUI()
        {
            uint lastCompletedLevelNumber = _playerDataController.PlayerData.LastCompletedLevelNumber;

            _scoreText.text = _playerDataController.PlayerData.Score.ToString();
            _currentLevelIndicatorText.text = (lastCompletedLevelNumber + 1).ToString();
            _nextLevelIndicatorText.text = (lastCompletedLevelNumber + 2).ToString();

            for (int i = 0; i < _levelController._currentLevelData.Pools.Count; i++)
                _poolIndicators.Add(Instantiate(GameSettings.Instance.poolIndicatorUiPrefab, _poolIndicatorsParent));
        }
    }
}
