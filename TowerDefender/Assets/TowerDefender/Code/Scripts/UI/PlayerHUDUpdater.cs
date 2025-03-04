using System.Collections.Generic;
using TowerDefender.Gameflow;
using TowerDefender.Player;
using TowerDefender.Units;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace TowerDefender.UI
{
    public sealed class PlayerHUDUpdater : MonoBehaviour, ISubscriber<GameStateChangedEvent>
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private TMPro.TextMeshProUGUI _moneyAmountText;

        [Header("Player Units")]
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private RectTransform _scrollViewContent;
        [SerializeField] private GameObject _unitButtonPrefab;
        [SerializeField] private PlayerBaseUnitModel[] _playerUnits;
        // TODO Not a fan to have a hard reference here
        [SerializeField] private PlayerUnitSpawner _playerUnitSpawner;

        private const string _MONEY_LEFT_TEXT = "GOLD: ";

        // TODO this could be data that we inject through SO or the monobehaviour directly for designer to tweak
        private readonly Dictionary<UnitType, string> _unitTypeToString = new Dictionary<UnitType, string>
        {
            { UnitType.PlayerCastle,            "Castle" },
            { UnitType.PlayerKnight,            "Knights" },
            { UnitType.PlayerFlamethrowerTower, "Flamethrower Tower" },
            { UnitType.PlayerArcherTower,       "Archers Tower" }
        };

        private void Awake()
        {
            _playerInventory.OnMoneyChanged += OnMoneyChanged;
            foreach (PlayerBaseUnitModel playerUnit in _playerUnits)
            {
                GameObject unitButton = Instantiate(_unitButtonPrefab, _scrollViewContent);
                if (!_unitTypeToString.TryGetValue(playerUnit.UnitType, out string unitTypeString))
                    throw new System.Exception($"UnitType {playerUnit.UnitType} not found in dictionary");

                unitButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = unitTypeString;
                unitButton.GetComponent<Button>().onClick.AddListener(() => _playerUnitSpawner.ChangeCurrentUnit(playerUnit.UnitType));
            }

            MessagingSystem<GameStateChangedEvent>.Subscribe(this);
        }

        private void Start()
        {
            OnMoneyChanged(_playerInventory.CurrentGold);
        }

        private void OnDestroy()
        {
            _playerInventory.OnMoneyChanged -= OnMoneyChanged;
            MessagingSystem<GameStateChangedEvent>.Unsubscribe(this);
        }

        private void OnMoneyChanged(int newAmount)
        {
            _moneyAmountText.text = _MONEY_LEFT_TEXT + newAmount.ToString();
        }

        public void OnEvent(GameStateChangedEvent evt)
        {
            _scrollView.SetActive(evt.NewState == GameStateEnum.Preparation);
        }
    }
}