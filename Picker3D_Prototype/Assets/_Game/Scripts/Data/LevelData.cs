using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Data/" + nameof(LevelData))]
    public class LevelData : ScriptableObject
    {
        //public uint LevelNumber;
        public List<PoolData> Pools;
    }

    [Serializable]
    public struct PoolData
    {
        public uint RequiredBallCount;
    }
}
