using UnityEngine;

namespace TowerDefender.Units
{
    public class UnitMovementSystem : BaseUnitSystem<UnitMovementModule, UnitBaseController>
    {
        public UnitMovementSystem(UnitBaseController owner, UnitMovementModule model) : base(owner, model)
        {
        }

        public override void UpdateSystem() 
        {
            
        }

        public override void ResetSystem()
        {
        }

        protected virtual void Move(Vector3 target)
        {
            Owner.SetPosition(Vector3.MoveTowards(Owner.Position, target, Model.MoveSpeed * Time.deltaTime));
        }
    }
}