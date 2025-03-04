using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_GamePaused", menuName = "TowerDefender/Gameflow/Game State Data/Game Paused")]
    public sealed class GameStateData_GamePaused : GameStateData
    {
        public override GameState CreateStateInstance()
        {
            return new GameState_GamePaused(this);
        }
    }
}