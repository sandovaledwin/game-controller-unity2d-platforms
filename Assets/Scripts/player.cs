using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;
    bool isRunning = false;
    bool isGoingARight = true; 

    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;
    [SerializeField]
    Transform groundCheckC;

    Object bulletRef;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public float jumpRate = 0.5F;
    private float nextJump = 0.0F;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRef = Resources.Load("bullet");
    }

    void FixedUpdate()
    {        

        isGrounded = isTouchingTheGround();

        animator.SetBool("isJumping", !isGrounded);

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(2, rb2d.velocity.y);
            spriteRenderer.flipX = false;
            isRunning = true;
            isGoingARight = true;
        } else if (Input.GetKey("a") || Input.GetKey("left")) {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
            spriteRenderer.flipX = true;
            isRunning = true;
            isGoingARight = false;
        } else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            isRunning = false;
        }

        if (Input.GetKey("s") && Time.time > nextFire)
        {
            animator.SetTrigger("shooting");
            isRunning = false;
            nextFire = Time.time + fireRate;
            makeOneShoot();
        }            


        if (Input.GetKey("space") && isGrounded && Time.time > nextJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 4);
            nextJump = Time.time + jumpRate;
        }

        if (isGrounded && isRunning)
        {
            animator.SetBool("isRunning", true);
        }

        if (isGrounded && !isRunning)
        {
            animator.SetBool("isRunning", false);
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

    void makeOneShoot()
    {
        GameObject bullet = (GameObject)Instantiate(bulletRef);

        object[] bulletConfig = new object[4];
        bulletConfig[0] = isGoingARight;
        bulletConfig[1] = transform.position.x;
        bulletConfig[2] = transform.position.y;

        bullet.SendMessage(
            "initBullet",
            bulletConfig
        );
    }

}
