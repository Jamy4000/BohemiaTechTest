using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/UnitCollection")]
    public class UnitCollection : ScriptableObject, System.IDisposable
    {
        private readonly List<UnitBaseController> _units = new List<UnitBaseController>(256);
        private readonly List<UnitBaseController> _unitsToRemove = new List<UnitBaseController>(256);

        public int ActiveUnitCount => _units.Count;
        public UnitBaseController GetUnit(int unitIndex) => _units[unitIndex];

        public System.Action OnAllUnitDestroyed;

        public virtual void Initialize() { }

        public virtual void Dispose() 
        {
            DestroyAllUnits();
        }

        public void AddUnit(UnitBaseController unit)
        {
            if (_units.Contains(unit))
                throw new System.Exception("Unit is already present in this collection.");

            _units.Add(unit);
        }

        public void RemoveUnit(UnitBaseController toRemove)
        {
            if (!_unitsToRemove.Contains(toRemove))
                _unitsToRemove.Add(toRemove);
        }

        public void UpdateAllUnits()
        {
            for (int unitIndex = 0; unitIndex < _units.Count; unitIndex++)
            {
                _units[unitIndex].ManualUpdate();
            }

            if (_unitsToRemove.Count > 0)
            {
                foreach (var unit in _unitsToRemove)
                {
                    _units.Remove(unit);
                }
                _unitsToRemove.Clear();

                if (_units.Count == 0)
                {
                    OnAllUnitDestroyed?.Invoke();
                }
            }

            // TODO Rebuild KDTree
        }

        public bool FindClosestTarget(Vector3 position, float targetSearchDistance, out ITarget currentTarget)
        {
            // TODO Implement KDTree or Quadtree for faster search
            currentTarget = null;
            float closestSqDistance = targetSearchDistance + float.Epsilon;
            closestSqDistance *= closestSqDistance;

            foreach (var unit in _units)
            {
                // Quick fix as the current system was targeting dead units. I know why, just don't have the time to fix it
                if (_unitsToRemove.Contains(unit))
                    continue;

                float sqDistance = unit.CalculateSqDistance(position);
                if (sqDistance < closestSqDistance)
                {
                    closestSqDistance = sqDistance;
                    currentTarget = unit;
                }
            }

            return currentTarget != null;
        }

        private void DestroyAllUnits()
        {
            for (int unitIndex = 0; unitIndex < _units.Count; unitIndex++)
            {
                _units[unitIndex].ManualDestroy();
                _units[unitIndex] = null;
            }
            _units.Clear();
        }
    }
}