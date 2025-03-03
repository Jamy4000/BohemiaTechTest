using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefender.Units
{
    public sealed class PlayerUnitSpawner : BaseUnitSpawner
    {
        [SerializeField] private InputActionReference _openFactoryMenuAction;
        [SerializeField] private InputActionReference _mousePositionAction;
        [SerializeField] private InputActionReference _placeUnitAction;

        private UnitType _currentlySelectedUnit = UnitType.PlayerArcherTower;

        protected override void Awake()
        {
            base.Awake();

            _placeUnitAction.action.performed += OnPlaceUnit;
        }

        private void OnDestroy()
        {
            _placeUnitAction.action.performed -= OnPlaceUnit;
        }


        private void OnPlaceUnit(InputAction.CallbackContext context)
        {
            if (_currentlySelectedUnit <= UnitType.FRIENDLY_START || _currentlySelectedUnit >= UnitType.FRIENDLY_END)
            {
                throw new Exception($"Invalid unit type selected: {_currentlySelectedUnit}");
            }

            Vector2 mousePosition = _mousePositionAction.action.ReadValue<Vector2>();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit, Mathf.Infinity) && hit.collider.CompareTag("Ground"))
            {
                // TODO Check that there is free space to place the unit
                // TODO Switch to a grid based system for placing units
                SpawnUnit(_currentlySelectedUnit, hit.point);
            }
        }
    }
}
