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
        public LevelData _currentLevelData;
        public LevelProgressInfo _levelProgressInfo;

        [SerializeField] private PlayerDataController _playerDataController;

        [SerializeField] private Transform _levelContainer;
        [SerializeField] private GameObject _levelSuccessParticlesContainer;

        private GameObject _currentLevelObject;
        private uint _currentLevelNumber;

        private void Start()
        {
            _levelProgressInfo = new();
        }

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onPlayerDataLoaded.Subscribe(GoToNextLevel, status);
            CoreSignals.Instance?.onNextLevelButtonClick.Subscribe(GoToNextLevel, status);

            CoreSignals.Instance?.onPoolClosed.Subscribe(OnPoolClosed, status);
            CoreSignals.Instance?.onPoolInteractedWithBall.Subscribe(OnPoolInteractedWithBall, status);

            CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);
            CoreSignals.Instance?.onRetryButtonClick.Subscribe(ResetLevel, status);
            CoreSignals.Instance?.onReachedToFinishArea.Subscribe(OnReachedToFinishArea, status);
        }

        private void OnPoolInteractedWithBall(bool status)
        {
            _levelProgressInfo.CollectedBallCount += (uint)(status ? 1 : -1);
        }

        private void OnReachedToFinishArea()
        {
            CoreSignals.Instance.onLevelCompleted?.Invoke(
                    new()
                    {
                        IsSuccess = true,
                        LevelNumber = _currentLevelNumber,
                        Score = _levelProgressInfo.CollectedBallCount * 10
                    });
        }

        private void OnPoolClosed(bool isFilled)
        {
            if (!isFilled)
                CoreSignals.Instance.onLevelCompleted?.Invoke(
                    new() { LevelNumber = _currentLevelNumber, IsSuccess = false });
            else
                _levelProgressInfo.FilledPoolCount++;
        }

        private void GoToNextLevel()
        {
            _currentLevelNumber = _playerDataController.PlayerData.LastCompletedLevelNumber + 1;

            Destroy(_currentLevelObject);

            var calculatedLevelNumber = CalculateLevelNumber(_currentLevelNumber);

            StartCoroutine(Helpers.ExecuteOnEndOfFrame(() => SpawnLevel(calculatedLevelNumber)));

            _levelProgressInfo = new() { LevelNumber = _currentLevelNumber };
        }

        private void ResetLevel()
        {
            Destroy(_currentLevelObject);

            var calculatedLevelNumber = CalculateLevelNumber(_currentLevelNumber);

            StartCoroutine(Helpers.ExecuteOnEndOfFrame(() => SpawnLevel(calculatedLevelNumber)));
        }

        private void SpawnLevel(uint levelNum)
        {
            string levelPath = Path.Combine(GameSettings.Instance.levelPrefabsResourcePath,
                                          string.Format(GameSettings.Instance.levelPrefabNameFormat, levelNum));

            string levelDataPath = Path.Combine(GameSettings.Instance.levelDataResourcePath,
                                          string.Format(GameSettings.Instance.levelDataNameFormat, levelNum));

            var level = Resources.Load<GameObject>(levelPath);
            _currentLevelObject = Instantiate(level, _levelContainer);

            var levelData = Resources.Load<LevelData>(levelDataPath);
            _currentLevelData = levelData;

            CoreSignals.Instance.onLevelSpawned?.Invoke(levelData);
        }

        private void OnLevelCompleted(LevelCompletionInfo levelCompletionData) =>
            _levelSuccessParticlesContainer.SetActive(levelCompletionData.IsSuccess);

        private void OnLevelSpawned(LevelData data) =>
            _levelSuccessParticlesContainer.SetActive(false);

        private uint CalculateLevelNumber(uint actualLevelNumber)
        {
            uint maxLevelNumber = GameSettings.Instance.maxLevelNumber;

            return actualLevelNumber > maxLevelNumber ?
                   (uint)(UnityEngine.Random.Range(0, maxLevelNumber - 1) + 1) : actualLevelNumber;
        }

    }
}
