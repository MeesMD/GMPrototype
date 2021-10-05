using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collision coll;

    [Range(5, 20)]
    public float speed = 10;

    public float jumpVel = 5;
    public float slideSpeed = 5;

    public bool canMove;
    public bool wallGrab;
    public bool wallSlide;

    public int side = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
    }

    void Update()
	{
		float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x,y);

        Walk(dir);

        if (coll.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            
            wallGrab = true;
            wallSlide = false;
        }

        if (wallGrab)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onWall && !coll.onGround)
		{
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
		}

        if (!coll.onWall || coll.onGround)
            wallSlide = false;


        if (Input.GetButtonDown("Jump"))
        {
            if (coll.onGround)
                Jump(Vector2.up);

            //WallJumping
            /*
            if (coll.onWall && !coll.onGround)
                WallJump();
            */
        }
    }

    private void WallSlide()
	{
        //if (!canMove)
        //   return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
	}

    private void Jump(Vector2 dir)
	{
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVel;
    }

    void Walk(Vector2 dir)
	{
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
	}
}
