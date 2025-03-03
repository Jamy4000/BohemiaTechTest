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
                // and we cannot find one that's close enough
                if (!Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out CurrentTarget))
                {
                    // We default to the castle
                    CurrentTarget = PlayerCastleController.PlayerCastle;
                }

                CurrentTarget.OnDeath += OnCurrentTargetDied;
            }
            // If we were targeting the castle
            else if (CurrentTarget == PlayerCastleController.PlayerCastle)
            {
                // and we can find a closer target
                if (Model.EnemyTargetsCollection.FindClosestTarget(Position, Model.TargetSearchDistance, out Utils.ITarget newTarget))
                {
                    // We unregister to the Castle's death event
                    CurrentTarget.OnDeath -= OnCurrentTargetDied;
                    // And register to the new target's death event
                    newTarget.OnDeath += OnCurrentTargetDied;
                    CurrentTarget = newTarget;
                }
            }
        }
    }
}