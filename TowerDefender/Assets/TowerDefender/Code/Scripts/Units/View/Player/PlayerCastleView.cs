using UnityEngine;

namespace TowerDefender.Units
{
    public sealed class PlayerCastleView : PlayerUnitBaseView
    {
        [field: SerializeField] public Transform GateTransform { get; private set; }
    }
}