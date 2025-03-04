using TowerDefender.Gameflow;

namespace TowerDefender
{
    public readonly struct GameStateChangedEvent
    {
        public readonly GameStateEnum OldState;
        public readonly GameStateEnum NewState;

        public GameStateChangedEvent(GameStateEnum oldState, GameStateEnum newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}