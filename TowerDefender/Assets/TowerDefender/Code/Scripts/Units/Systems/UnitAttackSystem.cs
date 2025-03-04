namespace TowerDefender.Units
{
    public class UnitAttackSystem : BaseUnitSystem<UnitAttackModule, UnitBaseController>
    {
        private float _attackTimer;

        public UnitAttackSystem(UnitBaseController owner, UnitAttackModule model) : base(owner, model)
        {
        }

        public override void OnEnable()
        {
            _attackTimer = 0f;
        }

        public override void UpdateSystem()
        {
            if (_attackTimer < Model.AttackCooldown)
            {
                _attackTimer += UnityEngine.Time.deltaTime;
            }
            else if (Owner.CurrentTarget.CalculateSqDistance(Owner.Position) < Model.AttackRangeSq)
            {
                Attack();
            }
        }

        public override void OnDisable()
        {
        }

        public override void Dispose()
        {
        }

        private void Attack()
        {
            _attackTimer = 0f;
            Owner.CurrentTarget.TakeDamage(Model.AttackDamage);
        }
    }
}