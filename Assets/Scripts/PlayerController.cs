using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    bool doubleJump;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                doubleJump = true;
            }
            else if(doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                doubleJump = false;
            }
        }

        if(horizontal == -1)
        {
            sr.flipX = true;
        }
        else if(horizontal == 1)
        {
            sr.flipX= false;
        }

        if (isGrounded())
        {
            an.SetFloat("speed", Mathf.Abs(horizontal));
        }
        else
        {
            an.SetFloat("speed", 0);
        }

        if(transform.position.y < -30)
        {
            transform.position = Vector2.zero;
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
