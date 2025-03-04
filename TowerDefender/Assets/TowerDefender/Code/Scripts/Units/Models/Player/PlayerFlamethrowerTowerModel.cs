using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "Flamethrower Tower Model", menuName = "TowerDefender/Units/Player/Flamethrower Tower Model")]
    public sealed class PlayerFlamethrowerTowerModel : PlayerBaseUnitModel
    {
        public override UnitBaseController CreateController()
        {
            return new PlayerFlamethrowerTowerController(this);
        }
    }
}