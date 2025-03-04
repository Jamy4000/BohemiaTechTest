namespace TowerDefender.Units
{
    public abstract class EnemyBaseController : UnitBaseController
    {
        public EnemyBaseController(UnitBaseModel model) : base(model)
        {
        }

        public override void ManualUpdate()
        {
            CheckCurrentTarget();
            base.ManualUpdate();
        }

        private void CheckCurrentTarget()
        {
            // If we currently have no target
            if (CurrentTarget == null)
            {
                // and we find one that's close enough
                if (Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out Utils.ITarget newTarget))
                {
                    UpdateCurrentTarget(newTarget);
                }
                else
                {
                    // Otherwise we default to the castle
                    UpdateCurrentTarget(PlayerCastleController.Instance);
                }
            }
            // If we were targeting the castle
            else if (CurrentTarget == PlayerCastleController.Instance)
            {
                // and we can find a closer target
                if (Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out Utils.ITarget newTarget))
                {
                    // We unregister to the Castle's death event
                    CurrentTarget.OnDeath -= OnCurrentTargetDied;
                    UpdateCurrentTarget(newTarget);
                }
            }
        }

        public override void Enable()
        {
            UpdateCurrentTarget(PlayerCastleController.Instance);

            base.Enable();
        }

        public override void Disable()
        {
            UpdateCurrentTarget(null);
            base.Disable();
        }
    }
}