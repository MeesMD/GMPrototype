using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVel = 5;

    private Rigidbody2D rb;
    private float fallMult = 2.5f;
    private float lowJumpMult = 2f;

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

        if(rb.velocity.y < 0)
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
		}
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }
    }
}
