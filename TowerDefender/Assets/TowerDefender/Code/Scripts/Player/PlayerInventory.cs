using System;
using UnityEngine;

namespace TowerDefender.Player
{
    [CreateAssetMenu(menuName = "TowerDefender/Player/PlayerInventory")]
    public sealed class PlayerInventory : ScriptableObject
    {
        [field: SerializeField] public int StartingGold { get; private set; } = 200;
        public int CurrentGold { get; private set; } = 0;

        public Action<int> OnMoneyChanged;

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
    }
}