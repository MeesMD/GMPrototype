using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collision coll;

    [Range(5, 20)]
    public float speed = 10;

    public float jumpForce;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    public bool canMove;
    public bool wallGrab;
    public bool wallSlide;
    public bool wallJumped;
    public bool isDashing;

    private bool groundTouch;
    private bool hasDashed;

    public ParticleSystem jumpParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        canMove = true;
    }

    void Update()
	{
		float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x,y);

        Walk(dir);

        if (coll.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            wallGrab = true;
            wallSlide = false;
		}
		else
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

        if (wallGrab)
        {
            rb.gravityScale = 0;
			if (x > .2f || x < -.2f)
				rb.velocity = new Vector2(rb.velocity.x, 0);

			float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
		{
            rb.gravityScale = 3;
		}

		if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (coll.onGround)
            wallJumped = false;


        if (Input.GetButtonDown("Jump"))
        {
            if (coll.onGround)
                Jump(Vector2.up);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());

        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3;
        GetComponent<BetterJump>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump(Vector2.up / 1.5f + wallDir / 1.5f);

        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void WallSlide()
	{
        if (!canMove)
           return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Jump(Vector2 dir)
	{
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void Walk(Vector2 dir)
	{
        if (!canMove)
            return;

        if (wallJumped)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
    }
}
