using Assets._Game.Scripts.Actors;
using UnityEngine;

namespace Assets._Game.Scripts.Managers
{
    public class UIManager : Manager<UIManager>
    {
        #region Actors

        [SerializeField] private UIPanelsController _uiPanelsController;

        #endregion
    }
}
