using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_PlayerWon : GameState<GameStateData_PlayerWon>
    {
        public GameState_PlayerWon(GameStateData_PlayerWon data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.PlayerWon;

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