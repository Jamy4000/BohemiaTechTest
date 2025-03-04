using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_MainMenu : GameState<GameStateData_MainMenu>
    {
        public GameState_MainMenu(GameStateData_MainMenu data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.MainMenu;

        public override bool CanBeEntered()
        {
            return true;
        }

        public override void StartState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void EndState()
        {
        }
    }
}