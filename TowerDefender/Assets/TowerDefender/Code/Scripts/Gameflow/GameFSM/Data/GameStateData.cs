using System.Collections.Generic;
using System;

using UnityEngine;

namespace TowerDefender.Gameflow
{
    /// <summary>
    /// Base classe holding the data for a Game State
    /// </summary>
    [Serializable]
    public abstract class GameStateData : ScriptableObject
    {
        [SerializeReference]
        public List<GameStateEnum> ExitStates = new();

        public abstract GameState CreateStateInstance();
    }
}