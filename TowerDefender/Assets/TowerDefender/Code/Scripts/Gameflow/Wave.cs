using System;
using TowerDefender.Units;
using UnityEngine;

namespace TowerDefender
{
    [CreateAssetMenu(fileName = "Wave", menuName = "TowerDefender/Gameflow/Wave", order = 1)]
    public class Wave : ScriptableObject
    {
        [Serializable]
        public struct WaveUnitData
        {
            public UnitType UnitType;
            public int UnitCount;
        }

        [field: SerializeField]
        public int WaveNumber { get; private set; } = -1;

        [field: SerializeField]
        public WaveUnitData[] UnitsToSpawn { get; private set; }

        [field: SerializeField]
        public int Reward { get; private set; } = 400;
    }
}