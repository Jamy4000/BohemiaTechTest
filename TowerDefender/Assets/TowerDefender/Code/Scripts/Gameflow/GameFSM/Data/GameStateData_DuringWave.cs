using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_DuringWave", menuName = "TowerDefender/Gameflow/Game State Data/During Wave")]
    public sealed class GameStateData_DuringWave : GameStateData
    {
        [field: SerializeField] public Wave[] Waves { get; private set; }

        public override GameState CreateStateInstance()
        {
            return new GameState_DuringWave(this);
        }
    }
}