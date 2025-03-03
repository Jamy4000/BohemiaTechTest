using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Modules/Health")]
    public sealed class UnitHealthModule : BaseUnitModule
    {
        [field: SerializeField]
        public int MaxHealth { get; private set; } = 100;

        public override UnitSystemType SystemType => UnitSystemType.Health;

        public override BaseUnitSystem CreateSystem(UnitBase owner)
        {
            return new UnitHealthSystem(owner, this);
        }
    }
}