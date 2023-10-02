﻿using Assets._Game.Scripts.Controllers.PanelControllers;
using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.IO;
using UnityEngine;

namespace Assets._Game.Scripts.Controllers
{
    public class LevelController : Actor<GameManager>
    {
        [SerializeField] private Transform _levelContainer;

        private uint _currentLevelNumber;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onPlayerDataLoaded.Subscribe(OnPlayerDataLoaded, status);

            //CoreSignals.Instance?.onLevelSpawned.Subscribe(OnLevelSpawned, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);
        }

        private void OnPlayerDataLoaded(PlayerData playerData)
        {
            _currentLevelNumber = GetNextLevelNumberByPlayerData(playerData);

            SpawnCurrentLevel();
        }

        private void SpawnCurrentLevel()
        {
            string levelPath = Path.Combine(GameSettings.Instance.levelPrefabsResourcePath,
                                          string.Format(GameSettings.Instance.levelPrefabNameFormat, _currentLevelNumber));

            string levelDataPath = Path.Combine(GameSettings.Instance.levelDataResourcePath,
                                          string.Format(GameSettings.Instance.levelDataNameFormat, _currentLevelNumber));


            var level = Resources.Load<Transform>(levelPath);
            Instantiate(level, _levelContainer);

            var levelData = Resources.Load<LevelData>(levelDataPath);

            CoreSignals.Instance.onLevelSpawned?.Invoke(levelData);
        }


        //private void OnLevelSpawned(LevelData levelData)
        //{

        //}

        private void OnLevelCompleted(bool isSuccess)
        {

        }

        private uint GetNextLevelNumberByPlayerData(PlayerData playerData)
        {
            uint maxLevelNumber = GameSettings.Instance.maxLevelNumber;

            uint lastlevelNumber = playerData.LastCompletedLevelNumber;

            return !playerData.IsReachedToMaxLevel ? lastlevelNumber + 1
                                                   : (uint)(UnityEngine.Random.Range(0, maxLevelNumber) + 1);
        }

    }
}
