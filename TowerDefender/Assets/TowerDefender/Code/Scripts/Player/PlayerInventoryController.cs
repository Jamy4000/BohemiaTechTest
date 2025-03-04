using UnityEngine;

namespace TowerDefender.Player
{
    public sealed class PlayerInventoryController : MonoBehaviour, Utils.ISubscriber<WaveEndedEvent>
    {
        [SerializeField] private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory.Earn(_playerInventory.StartingGold);
            Utils.MessagingSystem<WaveEndedEvent>.Subscribe(this);
        }

        private void OnDestroy()
        {
            Utils.MessagingSystem<WaveEndedEvent>.Unsubscribe(this);
            // Cleanup since this is a SO
            _playerInventory.Spend(_playerInventory.CurrentGold);
        }

        public void OnEvent(WaveEndedEvent evt)
        {
            _playerInventory.Earn(evt.Wave.Reward);
        }
    }
}