using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGadget;
    public FinalShipTrigger pickupPlayerManager;
    public CraftBenchMajiger craftBenchMajiger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isGadget)
            {
                craftBenchMajiger.gadgetPickupAmount += 1;
            }
            else
            {
                pickupPlayerManager.finalPickupAmount += 1;
            }
        }
        Destroy(gameObject);
    }
}
