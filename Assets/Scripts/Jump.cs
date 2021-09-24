using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVel = 5;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		if (Input.GetButtonDown("Jump"))
		{
            rb.velocity = Vector2.up * jumpVel;
		}
    }
}
