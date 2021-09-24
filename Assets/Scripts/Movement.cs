using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Range(5, 20)]
    public float speed = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
	{
		float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x,y);

        Walk(dir);
	}

    void Walk(Vector2 dir)
	{
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
	}
}
