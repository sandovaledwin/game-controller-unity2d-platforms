using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;
    [SerializeField]
    Transform groundCheckC;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {        

        isGrounded = isTouchingTheGround();
        if (!isGrounded) animator.Play("player_jump");

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(2, rb2d.velocity.y);
            if (isGrounded) animator.Play("player_run");
            spriteRenderer.flipX = false;
        } else if (Input.GetKey("a") || Input.GetKey("left")) {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
            if (isGrounded) animator.Play("player_run");
            spriteRenderer.flipX = true;
        } else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (isGrounded) animator.Play("player_idle");
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 4);
            animator.Play("player_jump");
        }
    }

    bool isTouchingTheGround()
    {

        if (Physics2D.Linecast(transform.position,groundCheckL.position,1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckC.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;

        }

        return false;
    }
}
