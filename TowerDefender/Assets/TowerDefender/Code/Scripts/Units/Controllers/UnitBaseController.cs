using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class UnitBaseController : Utils.IGenericPoolable, Utils.ITarget
    {

        private Utils.ITarget _currentTarget;
        public Utils.ITarget CurrentTarget => _currentTarget;

        public Action<Utils.ITarget> OnTargetChanged { get; set; }
        public Action<int> OnHit { get; set; }
        public Action OnDeath { get; set; }

        public readonly UnitBaseModel Model;
        public readonly UnitBaseView View;
        
        private readonly Dictionary<UnitModuleType, BaseUnitSystem> _systems;

        public UnitBaseController(UnitBaseModel model)
        {
            Model = model;
            View = GameObject.Instantiate(model.UnitViewPrefab).GetComponent<UnitBaseView>();

            int modulesLength = model.Modules.Length;
            _systems = new Dictionary<UnitModuleType, BaseUnitSystem>(modulesLength);
            for (int i = 0; i < modulesLength; i++)
            {
                var module = model.Modules[i];
                _systems.Add(module.ModuleType, module.CreateSystem(this));
            }

            OnDeath += OnUnitDied;
        }

        public virtual void ManualUpdate()
        {
            foreach (var kvp in _systems)
            {
                kvp.Value.UpdateSystem();
            }
        }

        public virtual void ManualDestroy()
        {
            Model.FriendlyTargetsCollection.RemoveUnit(this);
            foreach (var kvp in _systems)
            {
                kvp.Value.Dispose();
            }
        }

        public void SetPosition(Vector3 newPosition)
        {
            View.Transform.position = newPosition;
        }

        public BaseUnitSystem GetSystem(UnitModuleType moduleType)
        {
            return _systems[moduleType];
        }

        protected void UpdateCurrentTarget(Utils.ITarget newTarget)
        {
            if (_currentTarget != null)
                _currentTarget.OnDeath -= OnCurrentTargetDied;

            _currentTarget = newTarget;

            if (newTarget != null)
                newTarget.OnDeath += OnCurrentTargetDied;

            OnTargetChanged?.Invoke(newTarget);
        }

        /// <summary>
        /// Called when the current target for this unit has died
        /// </summary>
        protected void OnCurrentTargetDied()
        {
            UpdateCurrentTarget(null);
        }

        /// <summary>
        /// Called when this specific unit has died
        /// </summary>
        protected virtual void OnUnitDied()
        {
            OnShouldReturnToPool?.Invoke(this);
        }


        #region ITarget
        public virtual Vector3 Position => View.Transform.position;

        public float CalculateSqDistance(Vector3 from)
        {
            return (Position - from).sqrMagnitude;
        }

        public void TakeDamage(int damage)
        {
            OnHit?.Invoke(damage);
            if (_systems.TryGetValue(UnitModuleType.Health, out BaseUnitSystem unitSysten))
            {
                // TODO I hate that cast
                var healthSystem = unitSysten as UnitHealthSystem;
                View.OnHealthChanged(healthSystem.CurrentHealth, healthSystem.MaxHealth);
            }
        }
        #endregion ITarget


        #region IGenericPoolable
        public Action<Utils.IGenericPoolable> OnShouldReturnToPool { get; set; }

        public virtual void Enable()
        {
            View.gameObject.SetActive(true);

            foreach (var kvp in _systems)
            {
                kvp.Value.OnEnable();
            }

            Model.FriendlyTargetsCollection.AddUnit(this);
        }

        public virtual void Disable()
        {
            foreach (var kvp in _systems)
            {
                kvp.Value.OnDisable();
            }

            Model.FriendlyTargetsCollection.RemoveUnit(this);

            if (View != null)
                View.gameObject.SetActive(false);
        }

        public void Destroy()
        {
            GameObject.Destroy(View.gameObject);
        }
        #endregion IGenericPoolable
    }
}