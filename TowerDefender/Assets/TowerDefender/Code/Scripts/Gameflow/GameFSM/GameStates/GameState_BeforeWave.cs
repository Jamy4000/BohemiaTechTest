namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_BeforeWave : GameState<GameStateData_BeforeWave>
    {
        public GameState_BeforeWave(GameStateData_BeforeWave data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.BeforeWave;

        public override bool CanBeEntered()
        {
            return true;
        }

        public override void StartState()
        {
        }

        public override void EndState()
        {
        }
    }
}