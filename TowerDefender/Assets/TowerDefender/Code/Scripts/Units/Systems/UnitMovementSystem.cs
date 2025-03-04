using UnityEngine;
using UnityEngine.AI;

namespace TowerDefender.Units
{
    public class UnitMovementSystem : BaseUnitSystem<UnitMovementModule, UnitBaseController>
    {
        private readonly NavMeshAgent _agent;

        public UnitMovementSystem(UnitBaseController owner, UnitMovementModule model) : base(owner, model)
        {
            _agent = Owner.View.GetComponent<NavMeshAgent>();
            Owner.OnTargetChanged += OnTargetChanged;
        }

        public override void UpdateSystem() 
        {
        }

        public override void ResetSystem()
        {
        }

        public override void Dispose()
        {
            Owner.OnTargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged(Utils.ITarget target)
        {
            if (target == null)
            {
                _agent.isStopped = true;
                return;
            }

            Vector3 targetPosition = target.Position;
            Vector3 direction = Vector3.Normalize(targetPosition - Owner.Position);
            targetPosition -= direction * Model.DistanceFromEnemy;
            _agent.destination = targetPosition;
            _agent.isStopped = false;
        }

    }
}