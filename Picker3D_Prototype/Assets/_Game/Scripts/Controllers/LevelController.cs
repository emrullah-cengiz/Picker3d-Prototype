using Assets._Game.Scripts.Controllers.PanelControllers;
using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._Game.Scripts.Controllers
{
    public class LevelController : Actor<GameManager>
    {
        [SerializeField] private PlayerDataController _playerDataController;

        [SerializeField] private Transform _levelContainer;
        [SerializeField] private GameObject _levelSuccessParticlesContainer;

        private GameObject _currentLevelObject;
        private uint _currentLevelNumber;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onPlayerDataLoaded.Subscribe(GoToNextLevel, status);
            CoreSignals.Instance?.onNextLevelButtonClick.Subscribe(GoToNextLevel, status);

            //CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);
            CoreSignals.Instance?.onRetryButtonClick.Subscribe(ResetLevel, status);
        }

        private void GoToNextLevel()
        {
            _currentLevelNumber = GetNextLevelNumber(current: _playerDataController.PlayerData.LastCompletedLevelNumber);

            StartCoroutine(Helpers.ExecuteOnEndOfFrame(() => SpawnCurrentLevel()));
        }

        private void SpawnCurrentLevel()
        {
            uint levelNum = GetNextLevelNumber(current: _currentLevelNumber);

            string levelPath = Path.Combine(GameSettings.Instance.levelPrefabsResourcePath,
                                          string.Format(GameSettings.Instance.levelPrefabNameFormat, levelNum));

            string levelDataPath = Path.Combine(GameSettings.Instance.levelDataResourcePath,
                                          string.Format(GameSettings.Instance.levelDataNameFormat, levelNum));

            var level = Resources.Load<GameObject>(levelPath);
            _currentLevelObject = Instantiate(level, _levelContainer);

            var levelData = Resources.Load<LevelData>(levelDataPath);

            CoreSignals.Instance.onLevelSpawned?.Invoke(levelData);
        }

        private void OnLevelCompleted(uint levelNum, bool isSuccess) =>
            _levelSuccessParticlesContainer.SetActive(isSuccess);

        private void ResetLevel()
        {
            Destroy(_currentLevelObject);

            StartCoroutine(Helpers.ExecuteOnEndOfFrame(() => SpawnCurrentLevel()));
        }

        private uint GetNextLevelNumber(uint current)
        {
            uint maxLevelNumber = GameSettings.Instance.maxLevelNumber;

            return current > maxLevelNumber ?
                   (uint)(UnityEngine.Random.Range(0, maxLevelNumber) + 1) : _currentLevelNumber + 1;
        }

    }
}
