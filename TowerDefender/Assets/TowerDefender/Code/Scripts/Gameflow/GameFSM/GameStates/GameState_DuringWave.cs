using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_DuringWave : GameState<GameStateData_DuringWave>
    {
        private int _currentWaveIndex;

        public GameState_DuringWave(GameStateData_DuringWave data) : base(data)
        {
            _currentWaveIndex = -1;
        }

        public override GameStateEnum StateEnum => GameStateEnum.DuringWave;

        public override bool CanBeEntered()
        {
            return true;
        }

        public override void StartState()
        {
            _currentWaveIndex++;
            if (_currentWaveIndex == Data.Waves.Length)
            {
                RequestEnterState.Invoke(GameStateEnum.GameWon);
                return;
            }

            MessagingSystem<WaveStartedEvent>.Publish(new WaveStartedEvent(Data.Waves[_currentWaveIndex]));
        }

        public override void EndState()
        {
        }
    }
}