using UnityEngine;

namespace TowerDefender.Units
{
    [CreateAssetMenu(menuName = "TowerDefender/Units/Player Unit Collection")]
    public sealed class PlayerUnitCollection : UnitCollection
    {
        [SerializeField]
        private PlayerCastleModel _playerCastleModel;

        private PlayerCastleController _playerCastleController;
        public PlayerCastleController PlayerCastleController => _playerCastleController;

        public override void Initialize()
        {
            base.Initialize();

            _playerCastleController = _playerCastleModel.CreateController() as PlayerCastleController;
        }
    }
}