using TowerDefender.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace TowerDefender.Units
{
    public sealed class PlayerUnitSpawner : BaseUnitSpawner<PlayerBaseUnitModel>, ISubscriber<GameStateChangedEvent>
    {
        [SerializeField] private PlayerInventory _inventory;

        [SerializeField] private InputActionReference _mousePositionAction;
        [SerializeField] private InputActionReference _placeUnitAction;

        private PlayerBaseUnitModel _currentlySelectedUnit = null;

        protected override void Awake()
        {
            base.Awake();

            MessagingSystem<GameStateChangedEvent>.Subscribe(this);
            _currentlySelectedUnit = UnitPools[UnitType.PlayerArcherTower].UnitModel;
        }

        private void OnDestroy()
        {
            MessagingSystem<GameStateChangedEvent>.Unsubscribe(this);
        }


        private void OnPlaceUnit(InputAction.CallbackContext context)
        {
            if (_currentlySelectedUnit.UnitType <= UnitType.FRIENDLY_START || 
                _currentlySelectedUnit.UnitType >= UnitType.FRIENDLY_END)
            {
                throw new System.Exception($"Invalid unit type selected: {_currentlySelectedUnit}");
            }

            if (!_inventory.CanAfford(_currentlySelectedUnit.Price))
            {
                // TODO Player Feedback
                return;
            }

            Vector2 mousePosition = _mousePositionAction.action.ReadValue<Vector2>();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit, Mathf.Infinity) && hit.collider.CompareTag("Ground"))
            {
                // TODO Check that there is free space to place the unit
                // TODO Switch to a grid based system for placing units
                // TODO Maybe display a transparent version of the unit at the mouse position
                SpawnUnit(_currentlySelectedUnit.UnitType, hit.point);
                _inventory.Spend(_currentlySelectedUnit.Price);
            }
        }

        public void OnEvent(GameStateChangedEvent evt)
        {
            if (evt.NewState == Gameflow.GameStateEnum.Preparation)
                _placeUnitAction.action.performed += OnPlaceUnit;
            else if (evt.OldState == Gameflow.GameStateEnum.Preparation)
                _placeUnitAction.action.performed -= OnPlaceUnit;
        }

        public void ChangeCurrentUnit(UnitType playerUnitType)
        {
            _currentlySelectedUnit = UnitPools[playerUnitType].UnitModel;
        }
    }
}
