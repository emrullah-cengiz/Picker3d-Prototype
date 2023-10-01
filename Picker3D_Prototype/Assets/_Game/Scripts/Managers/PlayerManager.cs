using Assets._Game.Scripts.Actors;
using Assets._Game.Scripts.Signals;
using UnityEngine;

namespace Assets._Game.Scripts.Managers
{
    public class PlayerManager : Manager<PlayerManager>
    {
        [SerializeField] private Transform _playerParent;
        [SerializeField] private GameObject _playerObject;

        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance.onLevelSpawned.Subscribe(OnLevelSpawned, status);
        }

        private void OnLevelSpawned()
        {
            _playerObject.transform.localPosition = Vector3.zero;
        }
    }
}
