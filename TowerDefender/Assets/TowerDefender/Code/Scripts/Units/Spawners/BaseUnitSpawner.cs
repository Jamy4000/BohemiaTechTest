using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class BaseUnitSpawner<TUnitBase> : MonoBehaviour
        where TUnitBase : UnitBaseModel
    {
        [System.Serializable]
        private struct PoolData
        {
            public TUnitBase UnitModel;
            public int InitialSize;
            public int MaxSize;
            public bool CollectionChecks;
        }

        [SerializeField] private PoolData[] _poolData;

        protected Dictionary<UnitType, UnitPool<TUnitBase>> UnitPools { get; private set; }

        protected virtual void Awake()
        {
            UnitPools = new(_poolData.Length);
            for (int i = 0; i < _poolData.Length; i++)
            {
                UnitPool<TUnitBase> pool = new(_poolData[i].UnitModel, _poolData[i].InitialSize, _poolData[i].MaxSize, _poolData[i].CollectionChecks);
                UnitPools.Add(_poolData[i].UnitModel.UnitType, pool);
            }
        }

        public void SpawnUnit(UnitType type, Vector3 position)
        {
            UnitBaseController unit = UnitPools[type].RequestPoolableObject();
            unit.SetPosition(position);
        }
    }
}