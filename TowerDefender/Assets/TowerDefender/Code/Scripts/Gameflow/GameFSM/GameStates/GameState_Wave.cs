using Utils;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_Wave : GameState<GameStateData_Wave>
    {
        private int _currentWaveIndex;

        public GameState_Wave(GameStateData_Wave data) : base(data)
        {
            _currentWaveIndex = -1;
        }

        public override GameStateEnum StateEnum => GameStateEnum.Wave;

        public override bool CanBeEntered()
        {
            return true;
        }

        public override void StartState()
        {
            _currentWaveIndex++;
            Data.EnemyUnits.OnAllUnitDestroyed += OnAllEnemiesDestroyed;
            MessagingSystem<WaveStartedEvent>.Publish(new WaveStartedEvent(Data.Waves[_currentWaveIndex]));
        }

        public override void UpdateState()
        {
        }

        public override void EndState()
        {
            Data.EnemyUnits.OnAllUnitDestroyed -= OnAllEnemiesDestroyed;
        }

        private void OnAllEnemiesDestroyed()
        {
            if (_currentWaveIndex == Data.Waves.Length - 1)
            {
                RequestEnterState.Invoke(GameStateEnum.PlayerWon);
            }
            else
            {
                RequestEnterState.Invoke(GameStateEnum.Preparation);
            }
        }
    }
}