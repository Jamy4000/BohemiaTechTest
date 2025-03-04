using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Modules/Health")]
    public sealed class UnitHealthModule : BaseUnitModule
    {
        public override UnitModuleType ModuleType => UnitModuleType.Health;

        [field: SerializeField]
        public int MaxHealth { get; private set; } = 100;

        public override BaseUnitSystem CreateSystem(UnitBaseController owner)
        {
            return new UnitHealthSystem(owner, this);
        }
    }
}