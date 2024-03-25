namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A slow powerup.
    /// </summary>
    public class PowerupObjectSlow : PowerupObjectBase
    {
        protected override void PickUpPowerup(Collider player)
        {
            Debug.Log("[CT6RIGPR]: Picked up SLOW powerup");

            PowerupManager powerUpManager = player.GetComponent<PowerupManager>();
            if (powerUpManager != null)
            {
            }

            base.PickUpPowerup(player);
        }
    }
}
