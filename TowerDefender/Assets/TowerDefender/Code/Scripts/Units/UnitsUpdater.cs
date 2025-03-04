using UnityEngine;
using Utils;

namespace TowerDefender.Units
{
    [DefaultExecutionOrder(-100)] // Making sure this runs before anything else to initialize the UnitCollection
    public sealed class UnitsUpdater : MonoBehaviour, ISubscriber<GameStateChangedEvent>
    {
        [SerializeField]
        private UnitCollection _unitCollection;

        private void Awake()
        {
            MessagingSystem<GameStateChangedEvent>.Subscribe(this);
            _unitCollection.Initialize();
        }

        private void Update()
        {
            _unitCollection.UpdateAllUnits();
        }

        private void OnDestroy()
        {
            _unitCollection.DestroyAllUnits();
            MessagingSystem<GameStateChangedEvent>.Unsubscribe(this);
        }

        public void OnEvent(GameStateChangedEvent evt)
        {
            this.enabled = evt.NewState == Gameflow.GameStateEnum.Wave;
        }
    }
}