using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject player;
	private Vector3 playerStartPos;

	private void Start()
	{
		playerStartPos = player.transform.position;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			player.transform.position = playerStartPos;
		}
	}
}
