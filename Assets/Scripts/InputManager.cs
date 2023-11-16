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
    private bool flag_anda = false;

    public bool gameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(flag_anda){
            rb.velocity = new Vector2(Speed,0);
            // rb.AddForce(new Vector3(Speed,0, 0));
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        gameOver = true;
        rb.isKinematic = true;
    }

    public void OnJumpTap()
    {
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        
    }

    public void OnRightHold()
    {          
        flag_anda = true;
    }

    public void OnLeftHold()
    {
        flag_anda = false;
        rb.velocity = new Vector2(0,0);
    }
}
