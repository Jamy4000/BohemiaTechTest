using UnityEngine;

namespace TowerDefender.Units
{
    public class PlayerCastleController : PlayerUnitBaseController
    {
        public static PlayerCastleController PlayerCastle;

        private Vector3 _gatePosition;
        public override Vector3 Position => _gatePosition;

        public PlayerCastleController(PlayerCastleModel model) : base(model)
        {
            if (PlayerCastle != null)
            {
                throw new System.Exception("There can only be one player castle in the scene.");
            }

            PlayerCastle = this;
            // Adding the castle to the friendly targets collection from the start since it should be already placed in the scene
            model.FriendlyTargetsCollection.AddUnit(this);

            _gatePosition = (View as PlayerCastleView).GateTransform.position;
        }

        ~PlayerCastleController()
        {
            PlayerCastle = null;
        }

        protected override void OnUnitDied()
        {
            // FIREWORKS!
        }
    }
}