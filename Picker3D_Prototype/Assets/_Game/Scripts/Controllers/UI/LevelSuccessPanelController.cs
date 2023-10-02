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
    public class LevelSuccessPanelController : Actor<GameManager>
    {
        [SerializeField] private Button nextLevelBtn;

        protected override void ConfigureSubscriptions(bool status)
        {
            nextLevelBtn.onClick.Subscribe(OnNextLevelButtonClick, status);
        }

        private void OnNextLevelButtonClick()
        {
            CoreSignals.Instance.onNextLevelButtonClick?.Invoke();
        }
    }
}
