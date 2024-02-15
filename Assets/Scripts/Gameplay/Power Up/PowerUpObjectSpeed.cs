using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObjectSpeed : PowerUpObjectBase
{
    protected override void PickUpPowerUp(Collider player)
    {
        //reference the player's inventory. Add this to that inventory. Tie the inventory to the ball as thats the collider.

        PowerUpManager powerUpManager = player.GetComponent<PowerUpManager>();
        if (powerUpManager != null)
        {
            powerUpManager._speed++;
        }

        base.PickUpPowerUp(player);
    }


}
