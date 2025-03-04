using System;
using System.Collections.Generic;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Base class to represent a Game State.
    /// </summary>
    public abstract class GameState : Utils.IFSMState<GameStateEnum>
    {
        public abstract GameStateEnum StateEnum { get; }

        public bool IsActiveState { get; set; }
        public Action<GameStateEnum> RequestEnterState { get; set; }
        public Action RequestToExitState { get; set; }


        public abstract bool CanBeEntered();

        public abstract void StartState();

        public abstract void UpdateState();

        public abstract void EndState();

        public abstract bool HasPossibleTransitionsTo(GameStateEnum stateEnum);

        public abstract List<GameStateEnum> GetTransitionsStates();
    }

    /// <summary>
    /// Variant of the Player Move State, giving access to a generic PlayerMoveStateData.
    /// This should only communicate to the mover that we request to move.
    /// </summary>
    /// <typeparam name="TData"> The Type of Data used by this specific Move State </typeparam>
    public abstract class GameState<TData> : GameState
        where TData : GameStateData
    {
        public TData Data { get; }

        protected GameState(TData data)
        {
            Data = data;
        }

        public override bool HasPossibleTransitionsTo(GameStateEnum stateEnum)
        {
            return Data.ExitStates.Contains(stateEnum);
        }

        public override List<GameStateEnum> GetTransitionsStates()
        {
            return Data.ExitStates;
        }
    }
}