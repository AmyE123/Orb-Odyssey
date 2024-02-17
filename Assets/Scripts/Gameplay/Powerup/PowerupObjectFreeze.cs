namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A sticky powerup.
    /// </summary>
    public class PowerupObjectFreeze : PowerupObjectBase
    {
        protected override void PickUpPowerup(Collider player)
        {
            Debug.Log("[CT6RIGPR]: Picked up FREEZE powerup");

            PowerupManager powerUpManager = player.GetComponent<PowerupManager>();
            if (powerUpManager != null)
            {
                powerUpManager.AddCharge(Constants.PowerupType.Freeze);
            }

            base.PickUpPowerup(player);
        }
    }
}