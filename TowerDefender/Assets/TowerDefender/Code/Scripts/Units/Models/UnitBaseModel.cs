using UnityEngine;

namespace TowerDefender.Units
{
    public enum UnitType
    {
        NONE = -1,

        ENEMY_START = 0,

        EnemyArcher = 1,
        EnemyGrunt = 2,
        EnemyMage = 3,

        ENEMY_END = 2999,


        FRIENDLY_START = 3000,

        FRIENDLY_END = 5999,
    }

    public abstract class UnitBaseModel : ScriptableObject
    {
        [field: SerializeField]
        public UnitType UnitType { get; private set; } = UnitType.NONE;

        [field: SerializeField]
        public UnitBaseView UnitViewPrefab { get; private set; }

        /// <summary>
        /// The collection of friendly units that this unit belong to
        /// </summary>
        [field: SerializeField]
        public UnitCollection FriendlyTargetsCollection { get; private set; }

        [field: SerializeField]
        public BaseUnitModule[] Modules { get; private set; }

        [field: Header("Enemy Target Search"), SerializeField]
        public float TargetSearchDistance { get; private set; } = 10f;

        /// <summary>
        /// The collection of enemy units that this unit will target
        /// </summary>
        [field: SerializeField]
        public UnitCollection EnemyTargetsCollection { get; private set; }

        public abstract UnitBaseController CreateController();
    }
}