using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class PlayerBaseUnitModel : UnitBaseModel
    {
        [field: Header("Common Player Units Settings"), SerializeField]
        public int Price { get; private set; } = 50;
    }
}