﻿using System.Collections.Generic;

namespace Utils
{
    public abstract class FSM<TState, TStateEnum> : System.IDisposable
        where TState : class, IFSMState<TStateEnum>
        where TStateEnum : struct, System.Enum
    {
        public TState ActiveState { get; private set; }

        protected readonly Dictionary<TStateEnum, TState> States;

        private readonly List<TStateEnum> _currentStatePotentialTransitions = new(16);
        private readonly List<TStateEnum> _pendingTransitions = new(4);

        protected FSM(TState defaultState, List<TState> states)
        {
            States = new Dictionary<TStateEnum, TState>(states.Count);

            for (int stateIndex = 0; stateIndex < states.Count; stateIndex++)
            {
                RegisterNewState(states[stateIndex]);
            }
            StartNewState(defaultState);
        }

        public void ManualUpdate()
        {
            ActiveState.UpdateState();
        }

        public void ManualLateUpdate()
        {
            if (_pendingTransitions.Count == 0)
                return;

            // Used as a End of Frame Coroutine to check pending states and chose the one with the highest priority
            for (int stateIndex = 0; stateIndex < _currentStatePotentialTransitions.Count; stateIndex++)
            {
                // we go through each transitions of the state to check for the priority of each state.
                TStateEnum stateEnum = _currentStatePotentialTransitions[stateIndex];
                if (_pendingTransitions.Contains(stateEnum))
                {
                    StopCurrentState();
                    StartNewState(States[stateEnum]);
                    _pendingTransitions.Clear();
                    return;
                }
            }

#if DEBUG
            string transitions = string.Empty;
            for (int i = 0; i < _pendingTransitions.Count; i++)
            {
                transitions += _pendingTransitions[i] + ", ";
            }
            throw new System.Exception($"State {ActiveState.StateEnum} couldn't be exited. Pending Transitions: {transitions}");
#endif
        }

        public void Dispose()
        {
            StopCurrentState();
            ActiveState = null;

            foreach (TState state in States.Values)
            {
                if (state is System.IDisposable disposable)
                    disposable.Dispose();
            }

            States.Clear();
            _currentStatePotentialTransitions.Clear();
            _pendingTransitions.Clear();
        }

        public void RegisterNewState(TState stateToRegister)
        {
            if (States.ContainsKey(stateToRegister.StateEnum))
                throw new System.Exception($"State {stateToRegister.StateEnum} is already registered.");

            States.Add(stateToRegister.StateEnum, stateToRegister);
        }

        public void RegisterNewState(TStateEnum stateEnum, TState state)
        {
            if (States.ContainsKey(stateEnum))
                throw new System.Exception($"State {stateEnum} is already registered.");

            States.Add(stateEnum, state);
        }

        public void UnregisterState(TState stateToRemove)
        {
            if (!States.Remove(stateToRemove.StateEnum))
                throw new System.Exception($"State {stateToRemove.StateEnum} isn't registered.");
        }

        public void UnregisterState(TStateEnum stateToRemove)
        {
            if (!States.Remove(stateToRemove))
                throw new System.Exception($"State {stateToRemove} isn't registered.");
        }

        protected virtual void StartNewState(TState newState)
        {
            // StartState and prepare new State
            ActiveState = newState;
            ActiveState.IsActiveState = true;
            newState.StartState();
            newState.RequestToExitState += ExitCurrentState;
            newState.RequestEnterState += AddPendingState;

            _currentStatePotentialTransitions.AddRange(newState.GetTransitionsStates());
            // some states may be set as potential transitions, but may not be registered in the FSM yet.
            List<TStateEnum> finalTransitions = new List<TStateEnum>(_currentStatePotentialTransitions.Count);

            for (int stateIndex = 0; stateIndex < _currentStatePotentialTransitions.Count; stateIndex++)
            {
                TStateEnum stateEnum = _currentStatePotentialTransitions[stateIndex];
                if (!States.TryGetValue(stateEnum, out TState state))
                    continue;

                finalTransitions.Add(stateEnum);
                state.RequestEnterState += AddPendingState;
            }

            _currentStatePotentialTransitions.Clear();
            _currentStatePotentialTransitions.AddRange(finalTransitions);
        }

        protected virtual void StopCurrentState()
        {
            // EndState and Clean old stateEnum
            ActiveState.EndState();
            ActiveState.IsActiveState = false;
            ActiveState.RequestToExitState -= ExitCurrentState;

            for (int stateIndex = 0; stateIndex < _currentStatePotentialTransitions.Count; stateIndex++)
            {
                States[_currentStatePotentialTransitions[stateIndex]].RequestEnterState -= AddPendingState;
            }

            _currentStatePotentialTransitions.Clear();
        }

        protected virtual void AddPendingState(TStateEnum newStateEnum)
        {
            if (!ActiveState.HasPossibleTransitionsTo(newStateEnum))
                throw new System.Exception($"State {newStateEnum} isn't part of the possible transitions for {ActiveState.StateEnum}.");

            // The transition was already requested
            if (!_pendingTransitions.Contains(newStateEnum))
                _pendingTransitions.Add(newStateEnum);
        }

        private void ExitCurrentState()
        {
            for (int stateIndex = 0; stateIndex < _currentStatePotentialTransitions.Count; stateIndex++)
            {
                TStateEnum stateEnum = _currentStatePotentialTransitions[stateIndex];

                // The transition was already requested
                if (_pendingTransitions.Contains(stateEnum))
                    continue;

                TState state = States[stateEnum];
                if (state.CanBeEntered())
                {
                    _pendingTransitions.Add(stateEnum);
                    return;
                }
            }

            throw new System.Exception($"State {ActiveState} couldn't be exited.");
        }
    }
}