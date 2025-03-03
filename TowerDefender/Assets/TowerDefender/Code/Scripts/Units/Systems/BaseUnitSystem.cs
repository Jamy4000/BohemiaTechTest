namespace TowerDefender.Units
{
    public abstract class BaseUnitSystem
    {
        public abstract void UpdateSystem();
        public abstract void ResetSystem();
    }

    public abstract class BaseUnitSystem<TModel, TOwner> : BaseUnitSystem
        where TModel : BaseUnitModule
        where TOwner : UnitBaseController
    {
        protected readonly TModel Model;
        protected readonly TOwner Owner;

        public BaseUnitSystem(TOwner owner, TModel model) 
        {
            Owner = owner;
            Model = model;
            ResetSystem();
        }
    }
}