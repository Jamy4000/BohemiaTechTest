using System;
using UnityEngine;

namespace TowerDefender.Player
{
    [CreateAssetMenu(menuName = "TowerDefender/Player/PlayerInventory")]
    public sealed class PlayerInventory : ScriptableObject
    {
        [field: SerializeField] public int StartingGold { get; private set; } = 200;
        public int CurrentGold { get; private set; }
        public Action<int> OnMoneyChanged;

        private void Awake()
        {
            Initialize();
        }

        private void OnValidate()
        {
            Initialize();
        }

        public void Earn(int goldAmount)
        {
            CurrentGold += goldAmount;
            OnMoneyChanged?.Invoke(CurrentGold);
        }

        public bool CanAfford(int goldAmount)
        {
            return CurrentGold >= goldAmount;
        }

        public void Spend(int goldAmount)
        {
            CurrentGold -= goldAmount;
            OnMoneyChanged?.Invoke(CurrentGold);
        }

        private void Initialize()
        {
            CurrentGold = StartingGold;
            OnMoneyChanged?.Invoke(CurrentGold);
        }
    }

}