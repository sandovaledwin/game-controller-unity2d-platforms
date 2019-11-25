using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer sprite;
    float direction = 0f;
    bool isGoingToRight = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = !isGoingToRight;
        Invoke("DestroySelf",1f);
    }
    
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direction, 0);        
    }

    void initBullet(object args)
    {
        Vector3 initPosition;

        object[] config = (object[])args;

        bool isGoingAtRight = (bool)config[0];
        float xPosPlayer = (float)config[1];
        float yPosPlayer = (float)config[2];

        Debug.Log(isGoingAtRight);
        Debug.Log(xPosPlayer);
        Debug.Log(yPosPlayer);

        if (isGoingAtRight)
        {
            direction = 2f;
            initPosition = new Vector3(
                xPosPlayer + .4f,
                yPosPlayer + .2f,
                -1
            );
        } else
        {
            direction = -2f;
            initPosition = new Vector3(
                xPosPlayer - .4f,
                yPosPlayer + .2f,
                -1
            );
        }

        transform.position = initPosition;

        isGoingToRight = isGoingAtRight;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
