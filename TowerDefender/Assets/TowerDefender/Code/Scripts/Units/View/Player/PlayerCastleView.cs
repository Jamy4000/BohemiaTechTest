using UnityEngine;

namespace TowerDefender.Units
{
    public sealed class PlayerCastleView : UnitBaseView
    {
        [field: SerializeField] public Transform GateTransform { get; private set; }
    }
}