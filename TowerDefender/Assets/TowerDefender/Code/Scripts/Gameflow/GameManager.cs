using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TowerDefender.Gameflow
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateData[] _gameStateData;
        [SerializeField] private GameStateEnum _defaultState = GameStateEnum.BeforeWave;

        private GameFSM _fsm;

        private void Start()
        {
            InitializeFsm();
        }

        private void LateUpdate()
        {
            _fsm.ManualLateUpdate();
        }

        private void InitializeFsm()
        {
            int stateLength = _gameStateData.Length;
            List<GameState> states = new List<GameState>(stateLength);
            GameState defaultState = null;

            for (int i = 0; i < stateLength; i++)
            {
                GameState state = _gameStateData[i].CreateStateInstance();
                states.Add(state);

                if (state.StateEnum == _defaultState)
                    defaultState = state;
            }

            _fsm = new GameFSM(defaultState, states);
        }
    }
}