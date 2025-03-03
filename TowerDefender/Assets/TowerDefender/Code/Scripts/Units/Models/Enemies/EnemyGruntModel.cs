using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "EnemyGruntModel", menuName = "TowerDefender/Units/Enemies/Enemy Grunt Model")]
    public class EnemyGruntModel : UnitBaseModel
    {
        public override UnitBaseController CreateController()
        {
            return new EnemyGruntController(this);
        }
    }
}