using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Modules/Movement")]
    public sealed class UnitMovementModule : BaseUnitModule
    {
        public override UnitModuleType ModuleType => UnitModuleType.Movement;

        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 1f;

        [field: SerializeField]
        public float DistanceFromEnemy { get; private set; } = 1f;

        public override BaseUnitSystem CreateSystem(UnitBaseController owner)
        {
            return new UnitMovementSystem(owner, this);
        }
    }
}