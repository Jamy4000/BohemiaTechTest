using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "Castle Model", menuName = "TowerDefender/Units/Player/Castle Model")]
    public sealed class PlayerCastleModel : PlayerBaseUnitModel
    {
        [field: SerializeField]
        public Vector3 CastleSpawnPosition { get; private set; }

        public override UnitBaseController CreateController()
        {
            return new PlayerCastleController(this);
        }
    }
}