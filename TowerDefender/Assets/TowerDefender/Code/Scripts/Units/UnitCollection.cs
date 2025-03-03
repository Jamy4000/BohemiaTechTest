using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/UnitCollection")]
    public sealed class UnitCollection : ScriptableObject
    {
        private readonly List<UnitBaseController> _units = new List<UnitBaseController>(256);

        public void AddUnit(UnitBaseController unit)
        {
            if (_units.Contains(unit))
                throw new System.Exception("Unit is already present in this collection.");

            _units.Add(unit);
        }

        public void RemoveUnit(UnitBaseController unitBase)
        {
            _units.Remove(unitBase);
        }

        public bool FindClosestTarget(Vector3 position, float targetSearchDistance, out ITarget currentTarget)
        {
            // TODO Implement KDTree or Quadtree for faster search
            currentTarget = null;
            float closestSqDistance = targetSearchDistance + float.Epsilon;

            foreach (var unit in _units)
            {
                float sqDistance = unit.CalculateSqDistance(position);
                if (sqDistance < closestSqDistance)
                {
                    closestSqDistance = sqDistance;
                    currentTarget = unit;
                }
            }

            return currentTarget != null;
        }
    }
}