using System;
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
        
        private readonly BaseUnitSystem[] _systems;

        public UnitBaseController(UnitBaseModel model)
        {
            Model = model;
            View = GameObject.Instantiate(model.UnitViewPrefab).GetComponent<UnitBaseView>();

            int modulesLength = model.Modules.Length;
            _systems = new BaseUnitSystem[modulesLength];
            for (int i = 0; i < modulesLength; i++)
            {
                _systems[i] = model.Modules[i].CreateSystem(this);
            }

            OnDeath += OnUnitDied;
        }

        public virtual void ManualUpdate()
        {
            foreach (var system in _systems)
            {
                system.UpdateSystem();
            }
        }

        public virtual void ManualDestroy()
        {
            Model.FriendlyTargetsCollection.RemoveUnit(this);
            foreach (var system in _systems)
            {
                system.Dispose();
            }
        }

        public void SetPosition(Vector3 newPosition)
        {
            View.Transform.position = newPosition;
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
        }
        #endregion ITarget


        #region IGenericPoolable
        public Action<Utils.IGenericPoolable> OnShouldReturnToPool { get; set; }

        public void Enable()
        {
            foreach (var system in _systems)
            {
                system.OnEnable();
            }

            Model.FriendlyTargetsCollection.AddUnit(this);

            View.gameObject.SetActive(true);
        }

        public void Disable()
        {
            foreach (var system in _systems)
            {
                system.OnDisable();
            }

            Model.FriendlyTargetsCollection.RemoveUnit(this);

            View.gameObject.SetActive(false);
        }

        public void Destroy()
        {
            GameObject.Destroy(View.gameObject);
        }
        #endregion IGenericPoolable
    }
}