using UnityEngine;
using Utils;

namespace TowerDefender.Units
{
    public sealed class EnemyUnitWaveSpawner : BaseUnitSpawner, ISubscriber<WaveStartedEvent>
    {
        protected override void Awake()
        {
            base.Awake();
            MessagingSystem<WaveStartedEvent>.Subscribe(this);
        }

        private void OnDestroy()
        {
            MessagingSystem<WaveStartedEvent>.Unsubscribe(this);
        }

        public void OnEvent(WaveStartedEvent evt)
        {
            foreach (Wave.WaveUnitData unitData in evt.Wave.UnitsToSpawn)
            {
                for (int i = 0; i < unitData.UnitCount; i++)
                {
                    // TODO Change start position to be randomized
                    SpawnUnit(unitData.UnitType, transform.position);
                }
            }
        }
    }
}
