using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool grounded = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Left and right movement
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Flip character direction
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
        }

        // Animation parameters
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            grounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            grounded = false;
    }
}