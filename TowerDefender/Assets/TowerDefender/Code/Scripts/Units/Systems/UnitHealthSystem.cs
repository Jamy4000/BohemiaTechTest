namespace TowerDefender.Units
{
    public class UnitHealthSystem : BaseUnitSystem<UnitHealthModule, UnitBaseController>
    {
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0f;

        public UnitHealthSystem(UnitBaseController owner, UnitHealthModule model) : base(owner, model)
        {
            Owner.OnHit += DamageUnit;
        }

        ~UnitHealthSystem()
        {
            Owner.OnHit -= DamageUnit;
        }

        public override void UpdateSystem() { }

        public override void ResetSystem()
        {
            CurrentHealth = Model.MaxHealth;
        }

        public override void Dispose()
        {
        }

        private void DamageUnit(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Owner.OnDeath?.Invoke();
            }
        }
    }
}