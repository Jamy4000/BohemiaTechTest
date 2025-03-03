namespace TowerDefender.Units
{
    public class UnitAttackSystem : BaseUnitSystem<UnitAttackModule, UnitBaseController>
    {
        private float _attackTimer;

        public UnitAttackSystem(UnitBaseController owner, UnitAttackModule model) : base(owner, model)
        { 
        }

        public override void UpdateSystem()
        {
            if (_attackTimer < Model.AttackCooldown)
            {
                _attackTimer += UnityEngine.Time.deltaTime;
            }
            else if (Owner.GetCurrentTarget().CalculateSqDistance(Owner.Position) < Model.AttackRangeSq)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _attackTimer = 0f;
            Owner.GetCurrentTarget().TakeDamage(Model.AttackDamage);
        }

        public override void ResetSystem()
        {
            _attackTimer = 0f;
        }
    }
}