using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Controllers.PanelControllers
{
    public class PlayerDataController : Actor<GameManager>
    {
        public PlayerData PlayerData { get; private set; }

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance?.onGameStarted.Subscribe(OnGameStarted, status);
            CoreSignals.Instance?.onLevelCompleted.Subscribe(OnLevelCompleted, status);
        }

        private void OnLevelCompleted(bool isSuccess)
        {
            //PlayerData.LastCompletedLevelNumber = 
        }

        private void OnGameStarted()
        {
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
            PlayerData = new();

            string playerDataPath = Path.Combine(Application.persistentDataPath, GameSettings.Instance.playerDataFileName);

            if (File.Exists(playerDataPath))
            {
                string jsonData = File.ReadAllText(playerDataPath);

                if (!string.IsNullOrWhiteSpace(jsonData))
                    PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
            }

            CoreSignals.Instance?.onPlayerDataLoaded?.Invoke(PlayerData);
        }

        //private void SavePlayerData()
        //{
        //    string playerDataPath = Path.Combine(Application.persistentDataPath, GameSettings.Instance.playerDataFileName);

        //    string playerDataJson = JsonUtility.ToJson(PlayerData);

        //    File.WriteAllText(playerDataPath, playerDataJson);
        //}
    }
}
