using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_GamePaused : GameState<GameStateData_GamePaused>
    {
        public GameState_GamePaused(GameStateData_GamePaused data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.Pause;

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