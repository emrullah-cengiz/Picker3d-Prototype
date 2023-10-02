using Assets._Game.Scripts.Controllers.PanelControllers;
using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Controllers
{
    public class LevelSuccessPanelController : Actor<GameManager>
    {
        private LevelController _levelController;
        private PlayerDataController _playerDataController;

        [SerializeField] private Button _nextLevelBtn;
        [SerializeField] private TMP_Text _scoreText;

        void Start()
        {
            _levelController = FindObjectOfType<LevelController>();
            _playerDataController = FindObjectOfType<PlayerDataController>();

            _nextLevelBtn.interactable = false;

            StartCoroutine(GiveScore());
        }

        protected override void ConfigureSubscriptions(bool status)
        {
            _nextLevelBtn.onClick.Subscribe(OnNextLevelButtonClick, status);
        }

        public IEnumerator GiveScore()
        {
            const float shakeDuration = .05f;

            for (int i = 0; i < _levelController._levelProgressInfo.CollectedBallCount; i++)
            {
                var oldScore = _playerDataController.PlayerData.Score -
                            (_levelController._levelProgressInfo.CollectedBallCount * 10);

                _scoreText.text = (oldScore + (i + 1) * 10).ToString();

                if (i > 80)
                    continue;

                _scoreText.rectTransform.DOShakeScale(shakeDuration, .1f, 2, 10);

                yield return new WaitForSeconds(shakeDuration);
            }

            DOVirtual.DelayedCall(1f, () => _nextLevelBtn.interactable = true);
        }


        private void OnNextLevelButtonClick()
        {
            CoreSignals.Instance.onNextLevelButtonClick?.Invoke();
        }
    }
}
