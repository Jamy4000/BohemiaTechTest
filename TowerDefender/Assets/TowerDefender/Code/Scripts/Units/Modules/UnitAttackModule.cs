using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Modules/Attack")]
    public sealed class UnitAttackModule : BaseUnitModule
    {
        [field: SerializeField]
        public float AttackRange { get; private set; } = 1f;
        public float AttackRangeSq { get; private set; } = 1f;

        [field: SerializeField]
        public int AttackDamage { get; private set; } = 10;

        [field: SerializeField]
        public float AttackCooldown { get; private set; } = 3f;

        public override BaseUnitSystem CreateSystem(UnitBaseController owner)
        {
            return new UnitAttackSystem(owner, this);
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnValidate()
        {
            Initialize();
        }

        private void Initialize()
        {
            AttackRangeSq = AttackRange * AttackRange;
        }
    }
}