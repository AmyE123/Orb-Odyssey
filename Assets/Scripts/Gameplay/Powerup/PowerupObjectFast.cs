namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A fast powerup.
    /// </summary>
    public class PowerupObjectFast : PowerupObjectBase
    {
        protected override void PickUpPowerup(Collider player)
        {
            Debug.Log("[CT6RIGPR]: Picked up FAST powerup");

            PowerupManager powerUpManager = player.GetComponent<PowerupManager>();
            if (powerUpManager != null)
            {
                powerUpManager.AddCharge(Constants.PowerupType.Fast);
            }

            base.PickUpPowerup(player);
        }
    }
}
