using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "EnemyArcherModel", menuName = "TowerDefender/Units/Enemies/Enemy Archer Model")]
    public class EnemyArcherModel : UnitBaseModel
    {
        public override UnitBaseController CreateController()
        {
            return new EnemyArcherController(this);
        }
    }
}