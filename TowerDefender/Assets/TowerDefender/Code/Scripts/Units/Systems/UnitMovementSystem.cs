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
        }

        public override void OnEnable()
        {
            Owner.OnTargetChanged += OnTargetChanged;
        }

        public override void UpdateSystem() 
        {
        }

        public override void OnDisable()
        {
            Owner.OnTargetChanged -= OnTargetChanged;
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
                if (!_agent.isOnNavMesh && NavMesh.SamplePosition(Owner.Position, out NavMeshHit firstHit, 10f, -1))
                {
                    Owner.SetPosition(firstHit.position);
                }

                _agent.isStopped = true;
                return;
            }

            Vector3 targetPosition = target.Position;
            Vector3 direction = Vector3.Normalize(targetPosition - Owner.Position);
            targetPosition -= direction * Model.DistanceFromEnemy;

            // Quick fix for an error showing up in enditor that I don't think I'll be able to fix in time
            if (!_agent.isOnNavMesh && NavMesh.SamplePosition(Owner.Position, out NavMeshHit hit, 10f, -1))
            {
                Owner.SetPosition(hit.position);
            }
            _agent.SetDestination(targetPosition);
            _agent.isStopped = false;
        }
    }
}