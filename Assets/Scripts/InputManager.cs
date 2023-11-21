using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust the jump force in the Inspector

    public float Speed = 5f;
    private bool isJumping = false;
    private Rigidbody2D rb;
    public Rigidbody2D teto1;
    public Rigidbody2D teto2;
    public Rigidbody2D chao1;
    public Rigidbody2D chao2;
    public Rigidbody2D enemy;
    public Rigidbody2D espinho;
    public Rigidbody2D moeda;
    private bool flag_anda = false;

    public bool gameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // if(flag_anda){
        //     rb.velocity = new Vector2(Speed,0);
        // }

        // if (OnJumpTap()){
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }
        // rb.velocity = new Vector2(Speed,0);
        teto1.velocity = new Vector2(Speed,0);
        teto2.velocity = new Vector2(Speed,0);
        chao1.velocity = new Vector2(Speed,0);
        chao2.velocity = new Vector2(Speed,0);
        enemy.velocity = new Vector2(Speed,0);
        espinho.velocity = new Vector2(Speed,0);
        moeda.velocity = new Vector2(Speed,0);

        if (gameOver){
            SceneManager.LoadScene(1);
        }
    }

    //  void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isJumping = false;
    //     }
    // }

    void OnTriggerEnter2D(Collider2D collider)
    {
        gameOver = true;
        rb.isKinematic = true;
    }

    public void OnJumpTap()
    {   
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        Debug.Log("Apertou");
        
    }

    // public void OnRightHold()
    // {          
    //     flag_anda = true;
    // }

    public void OnLeftHold()
    {
        flag_anda = false;
        rb.velocity = new Vector2(0,0);
    }
}
