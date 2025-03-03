namespace TowerDefender.Units
{
    public enum UnitSystemType
    {
        Attack,
        Movement,
        Health
    }

    public abstract class BaseUnitSystem
    {
        public abstract void UpdateSystem();
        public abstract void ResetSystem();
    }

    public abstract class BaseUnitSystem<TModule> : BaseUnitSystem
        where TModule : BaseUnitModule
    {
        protected readonly TModule Model;
        protected readonly UnitBase Owner;

        public BaseUnitSystem(UnitBase owner, TModule model) 
        {
            Owner = owner;
            Model = model;
            ResetSystem();
        }
    }
}