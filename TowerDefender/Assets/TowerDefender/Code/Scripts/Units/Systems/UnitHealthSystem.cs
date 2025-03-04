namespace TowerDefender.Units
{
    public class UnitHealthSystem : BaseUnitSystem<UnitHealthModule, UnitBaseController>
    {
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0f;

        public UnitHealthSystem(UnitBaseController owner, UnitHealthModule model) : base(owner, model)
        {
        }

        public override void OnEnable()
        {
            CurrentHealth = Model.MaxHealth;
            Owner.OnHit += DamageUnit;
        }

        public override void UpdateSystem() { }

        public override void OnDisable()
        {
            Owner.OnHit -= DamageUnit;
        }

        public override void Dispose()
        {
            Owner.OnHit -= DamageUnit;
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