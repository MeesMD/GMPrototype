using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalShipTrigger : MonoBehaviour
{
    public GameObject particlesystems;
    public GameObject orb;
    public GameObject text0;
    public GameObject text1;
    public Movement movement;
    public TextMeshPro orbText;

    public float finalPickupAmount;

	void Update()
    {
        if (finalPickupAmount == 0)
        {
            orbText.text = "Find       to repair your ship!";
        }
        else if (finalPickupAmount == 1)
        {
            orbText.text = "Get on the ship and escape!";
            particlesystems.SetActive(false);
            orb.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && finalPickupAmount == 1)
        {
            text0.SetActive(false);
            text1.SetActive(true);
            movement.enabled = false;
        }
    }
}
