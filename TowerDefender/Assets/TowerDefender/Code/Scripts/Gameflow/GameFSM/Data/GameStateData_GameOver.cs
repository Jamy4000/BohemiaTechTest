using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_GameOver", menuName = "TowerDefender/Gameflow/Game State Data/Game Over")]
    public sealed class GameStateData_GameOver : GameStateData
    {
        public override GameState CreateStateInstance()
        {
            return new GameState_GameOver(this);
        }
    }
}