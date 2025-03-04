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
                if (Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out Utils.ITarget newTarget))
                    UpdateCurrentTarget(newTarget);
                else
                    return;
            }

            base.ManualUpdate();
        }
    }
}