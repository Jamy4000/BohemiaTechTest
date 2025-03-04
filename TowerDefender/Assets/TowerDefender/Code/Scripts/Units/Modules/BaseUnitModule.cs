using UnityEngine;

namespace TowerDefender.Units
{
    public enum UnitModuleType
    {
        Health,
        Movement,
        Attack
    }

    public abstract class BaseUnitModule : ScriptableObject
    {
        public abstract UnitModuleType ModuleType { get; }

        public abstract BaseUnitSystem CreateSystem(UnitBaseController owner);
    }
}