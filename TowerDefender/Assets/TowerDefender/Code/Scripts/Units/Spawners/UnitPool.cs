namespace TowerDefender.Units
{
    public sealed class UnitPool : Utils.GenericPoolHelper<UnitBaseController>
    {
        private readonly UnitBaseModel _unitModel;

        public UnitPool(UnitBaseModel unitModel, int initialSize, int maxSize, bool collectionChecks = false) : 
            base(initialSize, maxSize, collectionChecks)
        {
            _unitModel = unitModel;
        }

        protected override UnitBaseController CreatePooledItem()
        {
            return _unitModel.CreateController();
        }
    }
}