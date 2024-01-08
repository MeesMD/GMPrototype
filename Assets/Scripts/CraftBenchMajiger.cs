using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftBenchMajiger : MonoBehaviour
{
    public PickupPlayerManager pickupPlayerManager;
    public Movement movement;

    public GameObject craftBenchTUT;
    public TextMeshPro craftBenchTUTText;
    public GameObject gadgetTUT;
    public GameObject pressE;
    public GameObject mockBall;

    public float gadgetPickupAmount;

    private bool inRange;

    void Update()
    {
        if (gadgetPickupAmount == 1)
        {
            craftBenchTUTText.text = "Find 1      to craft gadget!";
        }
        else if (gadgetPickupAmount == 2)
        {
            craftBenchTUT.SetActive(false);
            mockBall.SetActive(false);
            pressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && inRange)
            {
                movement.canDash = true;
                pressE.SetActive(false);
                gadgetTUT.SetActive(true);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
