using UnityEngine.InputSystem;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Game State before the wave starts.
    /// </summary>
    public class GameState_Preparation : GameState<GameStateData_Preparation>
    {
        public GameState_Preparation(GameStateData_Preparation data) : base(data)
        {
        }

        public override GameStateEnum StateEnum => GameStateEnum.Preparation;

        public override bool CanBeEntered()
        {
            return true;
        }

        public override void StartState()
        {
            Data.UpdateTimer(Data.MaximumPreparationTime);
            Data.StartWaveAction.action.performed += OnStartWaveAction;
        }

        public override void UpdateState()
        {
            Data.UpdateTimer(Data.PreparationTimer - UnityEngine.Time.deltaTime);
            if (Data.PreparationTimer <= 0f)
            {
                RequestEnterState.Invoke(GameStateEnum.Wave);
            }
        }

        public override void EndState()
        {
            Data.StartWaveAction.action.performed -= OnStartWaveAction;
        }

        private void OnStartWaveAction(InputAction.CallbackContext context)
        {
            RequestEnterState.Invoke(GameStateEnum.Wave);
        }
    }
}