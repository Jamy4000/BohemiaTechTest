using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_PlayerWon", menuName = "TowerDefender/Gameflow/Game State Data/Player Won")]
    public sealed class GameStateData_PlayerWon : GameStateData
    {
        public override GameState CreateStateInstance()
        {
            return new GameState_PlayerWon(this);
        }
    }
}