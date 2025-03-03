using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class BaseUnitSpawner : MonoBehaviour
    {
        [System.Serializable]
        private struct PoolData
        {
            public UnitBaseModel UnitModel;
            public int InitialSize;
            public int MaxSize;
            public bool CollectionChecks;
        }

        [SerializeField] private PoolData[] _poolData;

        private Dictionary<UnitType, UnitPool> _unitPools;

        protected virtual void Awake()
        {
            _unitPools = new(_poolData.Length);
            for (int i = 0; i < _poolData.Length; i++)
            {
                UnitPool pool = new(_poolData[i].UnitModel, _poolData[i].InitialSize, _poolData[i].MaxSize, _poolData[i].CollectionChecks);
                _unitPools.Add(_poolData[i].UnitModel.UnitType, pool);
            }
        }

        public void SpawnUnit(UnitType type, Vector3 position)
        {
            UnitBaseController unit = _unitPools[type].RequestPoolableObject();
            unit.SetPosition(position);
        }
    }
}