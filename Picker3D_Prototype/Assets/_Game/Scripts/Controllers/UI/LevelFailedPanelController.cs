using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Controllers
{
    public class LevelFailedPanelController : Actor<GameManager>
    {
        [SerializeField] private Button retryBtn;

        protected override void ConfigureSubscriptions(bool status)
        {
            retryBtn.onClick.Subscribe(OnRetryButtonClick, status);
        }

        private void OnRetryButtonClick()
        {
            CoreSignals.Instance.onRetryButtonClick?.Invoke();
        }
    }
}
