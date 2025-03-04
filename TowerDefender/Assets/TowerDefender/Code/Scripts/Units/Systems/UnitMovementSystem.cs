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
                // Quick fix for an error showing up in enditor that I don't think I'll be able to fix in time
                if (_agent.isOnNavMesh)
                    _agent.isStopped = true;
                return;
            }

            Vector3 targetPosition = target.Position;
            Vector3 direction = Vector3.Normalize(targetPosition - Owner.Position);
            targetPosition -= direction * Model.DistanceFromEnemy;
            _agent.destination = targetPosition;
            // Quick fix for an error showing up in enditor that I don't think I'll be able to fix in time
            if (_agent.isOnNavMesh)
                _agent.isStopped = false;
        }

    }
}