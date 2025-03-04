using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace TowerDefender.Units
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class EnemyUnitWaveSpawner : BaseUnitSpawner<UnitBaseModel>, ISubscriber<WaveStartedEvent>
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
            Bounds bounds = GetComponent<BoxCollider>().bounds;

            foreach (Wave.WaveUnitData unitData in evt.Wave.UnitsToSpawn)
            {
                for (int i = 0; i < unitData.UnitCount; i++)
                {
                    Vector3 position = RandomPointInBounds(bounds);
                    if (NavMesh.SamplePosition(position, out NavMeshHit myNavHit, 100, -1))
                    {
                        SpawnUnit(unitData.UnitType, myNavHit.position);
                    }
                    else
                    {
                        throw new System.Exception("Couldn't find a valid point on NavMesh to spawn a unit.");
                    }
                }
            }
        }

        private static Vector3 RandomPointInBounds(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
