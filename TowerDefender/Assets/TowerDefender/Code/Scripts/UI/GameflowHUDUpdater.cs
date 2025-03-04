using System.Collections.Generic;
using TowerDefender.Gameflow;
using UnityEngine;
using Utils;

namespace TowerDefender.UI
{
    /// <summary>
    /// Not a big fan of updating all the UI in one big script for the game flow, but I'm running out of time
    /// </summary>
    public sealed class GameflowHUDUpdater : MonoBehaviour, ISubscriber<GameStateChangedEvent>
    {
        [SerializeField] private GameStateData_Preparation _gameStateDataPreparation = null;

        [SerializeField] private TMPro.TextMeshProUGUI _currentPhaseText = null;
        [SerializeField] private TMPro.TextMeshProUGUI _prepTimeRemainingText = null;
        [SerializeField] private TMPro.TextMeshProUGUI _timerText = null;
        [SerializeField] private TMPro.TextMeshProUGUI _holdSpaceBarHintText = null;

        // TODO this could be data that we inject through SO or the monobehaviour directly for designer to tweak
        private readonly Dictionary<GameStateEnum, string> _gameStateToText = new Dictionary<GameStateEnum, string>
        {
            { GameStateEnum.MainMenu,       "Main Menu" },
            { GameStateEnum.None,           "ERROR" },
            { GameStateEnum.Preparation,    "Prepare Yourself" },
            { GameStateEnum.Wave,           "Wave Incoming!" },
            { GameStateEnum.Pause,          "Pause" },
            { GameStateEnum.GameOver,       "Game Over" },
            { GameStateEnum.PlayerWon,      "Player Won" }
        };

        private void Awake()
        {
            MessagingSystem<GameStateChangedEvent>.Subscribe(this);
        }

        private void Update()
        {
            _timerText.text = _gameStateDataPreparation.PreparationTimer.ToString("F1") + "s";
        }

        private void OnDestroy()
        {
            MessagingSystem<GameStateChangedEvent>.Unsubscribe(this);
        }

        public void OnEvent(GameStateChangedEvent evt)
        {
            _currentPhaseText.text = _gameStateToText[evt.NewState];

            if (evt.NewState == GameStateEnum.Preparation)
            {
                this.enabled = true;
                _timerText.gameObject.SetActive(true);
                _holdSpaceBarHintText.gameObject.SetActive(true);
                _prepTimeRemainingText.gameObject.SetActive(true);
            }
            else
            {
                this.enabled = false;
                _timerText.gameObject.SetActive(false);
                _holdSpaceBarHintText.gameObject.SetActive(false);
                _prepTimeRemainingText.gameObject.SetActive(false);
            }
        }
    }
}