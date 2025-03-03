using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "Castle Model", menuName = "TowerDefender/Units/Player/Castle Model")]
    public sealed class PlayerCastleModel : UnitBaseModel
    {
        public override UnitBaseController CreateController()
        {
            return new PlayerCastleController(this);
        }
    }
}