using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_BeforeWave", menuName = "TowerDefender/Gameflow/Game State Data/Before Wave")]
    public sealed class GameStateData_BeforeWave : GameStateData
    {
        public override GameState CreateStateInstance()
        {
            return new GameState_BeforeWave(this);
        }
    }
}