using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefender.Gameflow
{
    [CreateAssetMenu(fileName = "GameStateData_Preparation", menuName = "TowerDefender/Gameflow/Game State Data/Preparation")]
    public sealed class GameStateData_Preparation : GameStateData
    {
        [field: SerializeField]
        public InputActionReference StartWaveAction { get; private set; }

        [field: SerializeField]
        public float MaximumPreparationTime { get; private set; } = 120f; // in seconds

        public float PreparationTimer { get; private set; }
        public void UpdateTimer(float newValue)
        {
            PreparationTimer = newValue;
        }

        public override GameState CreateStateInstance()
        {
            return new GameState_Preparation(this);
        }
    }
}