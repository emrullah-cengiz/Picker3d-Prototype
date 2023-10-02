using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Game.Scripts.Controllers
{
    public class PoolsController : Actor<GameManager>
    {
        protected override void ConfigureSubscriptions(bool status)
        {
            CoreSignals.Instance.onLevelSpawned.Subscribe(SetupPools, status);
        }

        private void SetupPools(LevelData levelData)
        {
            var poolGameObjects = GameObject.FindGameObjectsWithTag(GameSettings.Instance.poolTag);

            for (int i = 0; i < poolGameObjects.Length; i++)
            {
                var pool = poolGameObjects[i].GetComponent<PoolObject>();

                if (levelData.Pools.Count >= i + 1)
                    pool.Setup(levelData.Pools[i + 1]);
            }
        }
    }
}
