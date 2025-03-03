using UnityEngine;

namespace TowerDefender.Units
{
    public class UnitMovementSystem : BaseUnitSystem<UnitMovementModule>
    {
        public UnitMovementSystem(UnitBase owner, UnitMovementModule model) : base(owner, model)
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
            Owner.Transform.position = Vector3.MoveTowards(Owner.Transform.position, target, Model.MoveSpeed * Time.deltaTime);
        }
    }
}