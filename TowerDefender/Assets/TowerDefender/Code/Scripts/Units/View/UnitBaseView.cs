using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class UnitBaseView : MonoBehaviour
    {
        // Always faster to cache the transform, specially if we have a lot and we want to update them every frames
        public Transform Transform { get; private set; }

        protected virtual void Awake()
        {
            Transform = transform;
        }

        public abstract void OnHealthChanged(float newHealth, float maxHealth);
    }
}