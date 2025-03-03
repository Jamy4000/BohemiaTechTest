using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class UnitBase : MonoBehaviour, Utils.IGenericPoolable, Utils.ITarget
    {
        [SerializeField]
        private BaseUnitModule[] _modules;

        // For Modules to know what the current target is
        public Utils.ITarget CurrentTarget { get; protected set; }

        // Always faster to cache the transform, specially if we have a lot and we want to update them every frames
        public Transform Transform { get; private set; }

        public Action<int> OnHit { get; set; }
        public Action OnDeath { get; set; }

        private Dictionary<UnitSystemType, BaseUnitSystem> _systems;

        protected virtual void Awake()
        {
            Transform = transform;
            OnDeath += () => OnShouldReturnToPool?.Invoke(this);

            _systems = new Dictionary<UnitSystemType, BaseUnitSystem>(_modules.Length);
            for (int i = 0; i < _modules.Length; i++)
            {
                _systems.Add(_modules[i].SystemType, _modules[i].CreateSystem(this));
            }
        }

        protected virtual void Update()
        {
            // We need to make sure we have a target for the systems to run
            if (CurrentTarget == null)
            {
                if (TryFindNewTarget() == false)
                    return;
            }

            foreach (var kvp in _systems)
            {
                kvp.Value.UpdateSystem();
            }
        }

        private bool TryFindNewTarget()
        {
            // TODO
            return false;
        }

        #region ITarget
        public Vector3 Position => Transform.position;

        public float CalculateSqDistance(Vector3 from)
        {
            return (Position - from).sqrMagnitude;
        }

        public void TakeDamage(int damage)
        {
            OnHit?.Invoke(damage);
        }
        #endregion ITarget


        #region IGenericPoolable
        public Action<Utils.IGenericPoolable> OnShouldReturnToPool { get; set; }

        public void Enable()
        {
            foreach (var kvp in _systems)
            {
                kvp.Value.ResetSystem();
            }

            SearchForNewTarget();

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
        #endregion IGenericPoolable
    }

}