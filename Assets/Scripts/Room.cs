using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
		cam = transform.GetChild(0).gameObject;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && !collision.isTrigger)
		{
			cam.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			cam.SetActive(false);
		}
	}
}
