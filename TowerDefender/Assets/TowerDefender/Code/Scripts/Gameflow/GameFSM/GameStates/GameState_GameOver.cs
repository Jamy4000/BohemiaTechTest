using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_GameOver : GameState<GameStateData_GameOver>
    {
        public GameState_GameOver(GameStateData_GameOver data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.GameOver;

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