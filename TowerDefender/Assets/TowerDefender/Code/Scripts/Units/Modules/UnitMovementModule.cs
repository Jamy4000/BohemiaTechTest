using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Modules/Movement")]
    public sealed class UnitMovementModule : BaseUnitModule
    {
        [field: SerializeField]
        public float MoveSpeed { get; private set; }

        public override UnitSystemType SystemType => UnitSystemType.Movement;

        public override BaseUnitSystem CreateSystem(UnitBase owner)
        {
            return new UnitMovementSystem(owner, this);
        }
    }
}