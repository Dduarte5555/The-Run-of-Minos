using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust the jump force in the Inspector

    public float Speed = 5f;
    private bool isJumping = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void OnJumpTap()
    {
        if (!isJumping)
        {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        }
    }

    public void OnRightHold()
    {          
        rb.velocity = new Vector2(Speed,0);
    }

    public void OnLeftHold()
    {
         rb.velocity = new Vector2(-Speed,0);
    }
}
