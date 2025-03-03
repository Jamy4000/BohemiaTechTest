using Utils;

namespace TowerDefender.Units
{
    public abstract class PlayerUnitBaseController : UnitBaseController
    {
        public PlayerUnitBaseController(UnitBaseModel model) : base(model)
        {
        }

        public override void ManualUpdate()
        {
            // We need to make sure we have a target for the systems to run
            if (CurrentTarget == null)
            {
                if (TryFindNewTarget(out CurrentTarget))
                    CurrentTarget.OnDeath += OnCurrentTargetDied;
                else
                    return;
            }

            base.ManualUpdate();
        }

        private bool TryFindNewTarget(out ITarget newTarget)
        {
            return Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out newTarget);
        }
    }
}