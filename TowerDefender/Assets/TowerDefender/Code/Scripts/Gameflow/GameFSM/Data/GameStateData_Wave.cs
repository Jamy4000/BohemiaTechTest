using TowerDefender.Units;
using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_Wave", menuName = "TowerDefender/Gameflow/Game State Data/Wave")]
    public sealed class GameStateData_Wave : GameStateData
    {
        [field: SerializeField] public Wave[] Waves { get; private set; }

        [field: SerializeField] public UnitCollection EnemyUnits { get; private set; }

        public override GameState CreateStateInstance()
        {
            return new GameState_Wave(this);
        }
    }
}