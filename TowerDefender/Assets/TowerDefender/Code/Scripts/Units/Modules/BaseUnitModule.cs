using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class BaseUnitModule : ScriptableObject
    {
        public abstract UnitSystemType SystemType { get; }
        public abstract BaseUnitSystem CreateSystem(UnitBase owner);
    }
}