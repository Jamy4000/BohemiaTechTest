using System;
using UnityEngine;

namespace TowerDefender.Units
{
    public abstract class UnitBaseController : Utils.IGenericPoolable, Utils.ITarget
    {
        private Utils.ITarget _currentTarget;
        public Utils.ITarget CurrentTarget => _currentTarget;

        public Action<int> OnHit { get; set; }
        public Action OnDeath { get; set; }

        private readonly UnitBaseModel _model;
        private readonly UnitBaseView _view;
        private readonly BaseUnitSystem[] _systems;

        public UnitBaseController(UnitBaseModel model)
        {
            _model = model;
            _view = GameObject.Instantiate(model.UnitViewPrefab).GetComponent<UnitBaseView>();

            int modulesLength = model.Modules.Length;
            _systems = new BaseUnitSystem[modulesLength];
            for (int i = 0; i < modulesLength; i++)
            {
                _systems[i] = model.Modules[i].CreateSystem(this);
            }

            OnDeath += () => OnShouldReturnToPool?.Invoke(this);
        }

        public virtual void ManualUpdate()
        {
            // We need to make sure we have a target for the systems to run
            if (CurrentTarget == null)
            {
                if (TryFindNewTarget() == false)
                    return;
            }

            foreach (var system in _systems)
            {
                system.UpdateSystem();
            }
        }

        public virtual void OnManualDestroy()
        {
            _model.FriendlyTargetsCollection.RemoveUnit(this);
        }

        public void SetPosition(Vector3 newPosition)
        {
            _view.Transform.position = newPosition;
        }

        private bool TryFindNewTarget()
        {
            return _model.EnemyTargetsCollection.FindClosestTarget(Position, _model.TargetSearchDistance, out _currentTarget);
        }


        #region ITarget
        public Vector3 Position => _view.Transform.position;

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
                system.ResetSystem();
            }

            _model.FriendlyTargetsCollection.AddUnit(this);

            _view.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _model.FriendlyTargetsCollection.RemoveUnit(this);

            _view.gameObject.SetActive(false);
        }

        public void Destroy()
        {
            GameObject.Destroy(_view.gameObject);
        }
        #endregion IGenericPoolable
    }
}