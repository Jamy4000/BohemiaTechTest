namespace TowerDefender.Units
{
    public sealed class UnitPool<TUnitModel> : Utils.GenericPoolHelper<UnitBaseController>
        where TUnitModel : UnitBaseModel
    {
        public readonly TUnitModel UnitModel;

        public UnitPool(TUnitModel unitModel, int initialSize, int maxSize, bool collectionChecks = false) : 
            base(initialSize, maxSize, collectionChecks)
        {
            UnitModel = unitModel;
        }

        protected override UnitBaseController CreatePooledItem()
        {
            return UnitModel.CreateController();
        }
    }
}