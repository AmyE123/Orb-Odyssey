namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A sticky powerup.
    /// </summary>
    public class PowerupObjectSticky : PowerupObjectBase
    {
        protected override void PickUpPowerup(Collider player)
        {
            Debug.Log("[CT6RIGPR]: Picked up STICKY powerup");

            PowerupManager powerUpManager = player.GetComponent<PowerupManager>();
            if (powerUpManager != null)
            {
                powerUpManager.AddCharge(Constants.PowerupType.Sticky);
            }

            base.PickUpPowerup(player);
        }
    }
}