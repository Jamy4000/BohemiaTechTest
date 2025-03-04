namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Finite State machine to update the current state of the game.
    /// This class will handle the transition between the different Game States.
    /// </summary>
    public sealed class GameFSM : Utils.FSM<GameState, GameStateEnum>
    {
        public GameFSM(GameState defaultState, System.Collections.Generic.List<GameState> states) : base(defaultState, states)
        {
        }

        protected override void StartNewState(GameState newState)
        {
            GameStateEnum oldState = ActiveState == null ? GameStateEnum.None : ActiveState.StateEnum;

            base.StartNewState(newState);

            Utils.MessagingSystem<GameStateChangedEvent>.Publish(new (oldState, ActiveState.StateEnum));
        }
    }
}