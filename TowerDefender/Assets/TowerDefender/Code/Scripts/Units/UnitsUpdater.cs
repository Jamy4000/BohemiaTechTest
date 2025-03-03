using UnityEngine;

namespace TowerDefender.Units
{
    public sealed class UnitsUpdater : MonoBehaviour
    {
        [SerializeField]
        private UnitCollection _unitCollection;

        private void Update()
        {
            _unitCollection.UpdateAllUnits();
        }

        private void OnDestroy()
        {
            _unitCollection.DestroyAllUnits();
        }
    }
}