using UnityEngine;

namespace TowerDefender.Units
{
    public class PlayerCastleController : PlayerUnitBaseController
    {
        public static PlayerCastleController Instance;

        private Vector3 _gatePosition;
        public override Vector3 Position => _gatePosition;

        public PlayerCastleController(PlayerCastleModel model) : base(model)
        {
            if (Instance != null)
            {
                throw new System.Exception("There can only be one player castle in the scene.");
            }

            Instance = this;
            // Adding the castle to the friendly targets collection from the start since it should be already placed in the scene
            model.FriendlyTargetsCollection.AddUnit(this);
            
            View.Transform.position = model.CastleSpawnPosition;
            _gatePosition = (View as PlayerCastleView).GateTransform.position;
        }

        ~PlayerCastleController()
        {
            Instance = null;
        }

        protected override void OnUnitDied()
        {
            // FIREWORKS!
            // Overriding since we're not sending that one to the pool
        }
    }
}