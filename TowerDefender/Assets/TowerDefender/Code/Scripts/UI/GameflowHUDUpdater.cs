using System.Collections.Generic;
using TowerDefender.Gameflow;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField] private Button _restartGameButton = null;
        [SerializeField] private Button _quitGameButton = null;

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

            _restartGameButton.gameObject.SetActive(false);
            _restartGameButton.onClick.AddListener(() => ReloadScene());

            _quitGameButton.gameObject.SetActive(false);
            _quitGameButton.onClick.AddListener(() => Application.Quit());
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

            switch (evt.NewState)
            {
                case GameStateEnum.Preparation:
                    TogglePrepUI(true);
                    break;

                case GameStateEnum.PlayerWon:
                case GameStateEnum.GameOver:
                    TogglePrepUI(false);
                    _restartGameButton.gameObject.SetActive(true);
                    _quitGameButton.gameObject.SetActive(true);
                    break;

                default:
                    TogglePrepUI(false);
                    break;
            }
        }

        private void TogglePrepUI(bool isActive)
        {
            this.enabled = isActive;
            _timerText.gameObject.SetActive(isActive);
            _holdSpaceBarHintText.gameObject.SetActive(isActive);
            _prepTimeRemainingText.gameObject.SetActive(isActive);
        }

        private void ReloadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}