using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class InputManager : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust the jump force in the Inspector
    [SerializeField] private Canvas Canvas;
    public int coins = 0;
    public float tempo_power_up = 0F;
    public float tempo = 0F;
    public float tempo_recorde = 0F;
    private float time = 1.0F;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI recorde;
    public float Speed = 5f;
    private bool isJumping = false;
    private bool invincible = false;
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
        coinsText.text = "Coins: " + coins.ToString();
        recorde.text = tempo_recorde.ToString();
        recorde.text = " " + PlayerPrefs.GetFloat("Pontuacao", 1.0f).ToString();
        
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
        tempo_power_up = PlayerPrefs.GetFloat("SavedTime_powerup", 1.0f);
        tempo_power_up = tempo_power_up + Time.unscaledDeltaTime;
        PlayerPrefs.SetFloat("SavedTime_powerup", tempo_power_up);
        PlayerPrefs.Save(); 
        tempo = tempo + Time.unscaledDeltaTime;

        if (PlayerPrefs.GetFloat("OnGame", 1.0f) == 1){
            tempo_recorde = tempo_recorde + Time.unscaledDeltaTime;
            PlayerPrefs.SetFloat("Pontuacao", tempo_recorde);
            PlayerPrefs.Save();
            recorde.text = " " + tempo_recorde.ToString();
        }
        
        if ((tempo > 7) && (tempo_power_up > 5) && (PlayerPrefs.GetFloat("OnGame", 1.0f) == 1)){
            time = time * 1.1F;
            Time.timeScale = time;
            tempo = 0F;
            PlayerPrefs.SetFloat("SavedTime", time);
            PlayerPrefs.Save();
            Debug.Log("AUMENTEI VELO: " + time); 
            if (PlayerPrefs.GetFloat("Invincible", 3.0f) == 1){
                PlayerPrefs.SetFloat("Invincible", 0F);
                PlayerPrefs.Save();
            }


            FindObjectOfType<AudioManager>().Stop("SlowTime");
            FindObjectOfType<AudioManager>().Stop("Shield");
        }
        if (gameOver){
            Time.timeScale = 0;
            SceneManager.LoadScene("Scenes/MenuFinal");
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void Invincible()
    {
        //invincible = GetBoolFromPlayerPrefs(invincible);
        // invincible = PlayerPrefs.GetInt(invincible, 0);
        FindObjectOfType<AudioManager>().Play("Shield");
        invincible = true;
        PlayerPrefs.SetFloat("Invincible", 1F);
        PlayerPrefs.Save();
        time = PlayerPrefs.GetFloat("SavedTime", 1.0f);
        Time.timeScale = time;
        tempo_power_up = 0;
        PlayerPrefs.SetFloat("SavedTime_powerup", tempo_power_up);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save();
        Debug.Log("InvencicleSS: " + invincible); 
        Canvas.gameObject.SetActive(false);
        

        //SaveBoolToPlayerPrefs(invincible, invincible);
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {   Debug.Log("INCREIBEL: " + PlayerPrefs.GetFloat("Invincible", 3.0f));
        if (collider.gameObject.CompareTag("Enemy") && PlayerPrefs.GetFloat("Invincible", 3.0f) == 0)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");  
            gameOver = true;
        }

        if (collider.gameObject.CompareTag("Moeda"))
        {
            coins++;
            collider.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Coin");      
            coinsText.text = "Coins: " + coins.ToString();
            if(coins>=2){
                Time.timeScale = 0;
                coins = 0;
                PlayerPrefs.SetFloat("OnGame", 0);
                Canvas.gameObject.SetActive(true);
            }
        }
    }

    public void OnJumpTap()
    {   
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        FindObjectOfType<AudioManager>().Play("Jump");        
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
    public void OnSlowButton ()
    {
        FindObjectOfType<AudioManager>().Play("SlowTime");
        Debug.Log("Time: " + time);
        Time.timeScale = 0.5F;
        Debug.Log("Time: " + time);
        tempo_power_up = 0;
        PlayerPrefs.SetFloat("SavedTime_powerup", tempo_power_up);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save();
        Canvas.gameObject.SetActive(false); 
    }
}
