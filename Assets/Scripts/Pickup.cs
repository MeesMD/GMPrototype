using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGadget;
    public PickupPlayerManager pickupPlayerManager;
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
                pickupPlayerManager.finalPartPickupAmount += 1;
            }
        }
        Destroy(gameObject);
    }
}
