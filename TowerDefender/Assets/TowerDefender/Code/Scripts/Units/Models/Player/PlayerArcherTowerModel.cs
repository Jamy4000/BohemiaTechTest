using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "Archer Tower Model", menuName = "TowerDefender/Units/Player/Archer Tower Model")]
    public sealed class PlayerArcherTowerModel : PlayerBaseUnitModel
    {
        public override UnitBaseController CreateController()
        {
            return new PlayerArcherTowerController(this);
        }
    }
}