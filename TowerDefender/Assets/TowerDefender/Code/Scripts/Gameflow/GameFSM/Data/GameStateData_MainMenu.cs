using UnityEngine;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_MainMenu", menuName = "TowerDefender/Gameflow/Game State Data/Main Menu")]
    public sealed class GameStateData_MainMenu : GameStateData
    {
        public override GameState CreateStateInstance()
        {
            return new GameState_MainMenu(this);
        }
    }
}