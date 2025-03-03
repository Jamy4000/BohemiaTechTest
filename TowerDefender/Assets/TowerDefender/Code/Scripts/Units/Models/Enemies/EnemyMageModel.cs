using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(fileName = "EnemyMageModel", menuName = "TowerDefender/Units/Enemies/Enemy Mage Model")]
    public class EnemyMageModel : UnitBaseModel
    {
        public override UnitBaseController CreateController()
        {
            return new EnemyMageController(this);
        }
    }
}