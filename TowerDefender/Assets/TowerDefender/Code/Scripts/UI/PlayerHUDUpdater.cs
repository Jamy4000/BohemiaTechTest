using TowerDefender.Player;
using UnityEngine;

namespace TowerDefender.UI
{
    public sealed class PlayerHUDUpdater : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private TMPro.TextMeshProUGUI _moneyAmountText;

        private const string _MONEY_LEFT_TEXT = "GOLD: ";

        private void Awake()
        {
            _playerInventory.OnMoneyChanged += OnMoneyChanged;
        }

        private void Start()
        {
            OnMoneyChanged(_playerInventory.CurrentGold);
        }

        private void OnDestroy()
        {
            _playerInventory.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int newAmount)
        {
            _moneyAmountText.text = _MONEY_LEFT_TEXT + newAmount.ToString();
        }
    }
}