namespace TowerDefender.Units
{
    public abstract class BaseUnitSystem : System.IDisposable
    {
        public abstract void OnEnable();
        public abstract void UpdateSystem();
        public abstract void OnDisable();

        public abstract void Dispose();
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
        }
    }
}