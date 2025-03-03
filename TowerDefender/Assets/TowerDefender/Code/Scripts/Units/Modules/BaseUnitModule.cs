using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class BaseUnitModule : ScriptableObject
    {
        public abstract BaseUnitSystem CreateSystem(UnitBaseController owner);
    }
}