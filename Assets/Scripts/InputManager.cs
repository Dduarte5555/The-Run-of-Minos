using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class InputManager : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust the jump force in the Inspector
    [SerializeField] private Canvas canvasPowerUp;
    [SerializeField] private Canvas Canvas_Morte;
    [SerializeField] AdsInitializer adsInitializer;
    public int coins = 0;
    public float tempo_power_up = 0F;
    public float tempo = 0F;
    public float tempo_recorde = 0F;
    private float time = 1.0F;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI recorde;
    public float Speed = 5f;
    private bool isJumping = false;
    public bool propaganda = true;
    private bool isDashing = false;
    private Rigidbody2D rb;
    public Rigidbody2D teto1;
    public Rigidbody2D teto2;
    public Rigidbody2D chao1;
    public Rigidbody2D chao2;
    public Rigidbody2D enemy;
    public Rigidbody2D espinho;
    public Rigidbody2D moeda;
    private bool flag_anda = false;

    public SpriteRenderer shieldSprite;

    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
        coinsText.text = "Coins: " + coins.ToString();
        recorde.text = tempo_recorde.ToString();
        recorde.text = " " + PlayerPrefs.GetInt("Pontuacao", 1).ToString();
        PlayerPrefs.SetFloat("Dash", 0F);
        PlayerPrefs.SetFloat("CoinMagnet", 0F);
        PlayerPrefs.SetFloat("Invincible", 0F);
        
    }

    void Update()
    {
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
            PlayerPrefs.SetInt("Pontuacao", Mathf.RoundToInt(tempo_recorde));
            PlayerPrefs.Save();
            recorde.text = " " + Mathf.RoundToInt(tempo_recorde).ToString();
        }

        if (PlayerPrefs.GetFloat("CoinMagnet", 1.0f) == 1) {
            PlayerPrefs.SetFloat("CoinMagnet", 0f);

            GetComponent<CoinMagnet>().setIsActiveCoinMagnet(true);
        }

        if ((tempo_power_up > 5) && (PlayerPrefs.GetFloat("OnGame", 1.0f) == 1)){ 
            if (GetComponent<CoinMagnet>().getIsActiveCoinMagnet()) {
                GetComponent<CoinMagnet>().setIsActiveCoinMagnet(false);
            }
            FindObjectOfType<AudioManager>().Stop("CoinMagnet");
        }

        if ((tempo > 7) && (tempo_power_up > 2) && (PlayerPrefs.GetFloat("OnGame", 1.0f) == 1)) {

            time = time * 1.1F;
            Time.timeScale = time;
            tempo = 0F;
            PlayerPrefs.SetFloat("SavedTime", time);
            PlayerPrefs.Save();
            Debug.Log("AUMENTEI VELO: " + time);
            FindObjectOfType<AudioManager>().Stop("SlowTime");
        }

        if ((tempo_power_up > 2) && (PlayerPrefs.GetFloat("OnGame", 1.0f) == 1)){
            if (PlayerPrefs.GetFloat("Dash", 3.0f) == 1) {
                PlayerPrefs.SetFloat("Dash", 0F);
                PlayerPrefs.SetFloat("Invincible", 0F);
                FindObjectOfType<AudioManager>().Stop("Shield");
            }
            
        }
        if (PlayerPrefs.GetFloat("Invincible", 3.0f) == 1) { 
            shieldSprite.enabled = true;
        } else
        {
            shieldSprite.enabled = false;
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && PlayerPrefs.GetFloat("Dash", 3.0f) == 0)
        {
            if (PlayerPrefs.GetFloat("Invincible", 3.0f) == 1)
            {
                PlayerPrefs.SetFloat("Invincible", 0F);
                PlayerPrefs.Save();
                FindObjectOfType<AudioManager>().Stop("Shield");
            }
            else { 
                if (propaganda){
                    Time.timeScale = 0;
                    PlayerPrefs.SetFloat("OnGame", 0);
                    FindObjectOfType<AudioManager>().Stop("SlowTime");
                    PlayerPrefs.SetFloat("CoinMagnet", 0F);
                    FindObjectOfType<AudioManager>().Stop("CoinMagnet");
                    //adsInitializer.InitializeAds();
                    Canvas_Morte.gameObject.SetActive(true);

                }
                else{
                    FindObjectOfType<AudioManager>().Play("GameOver");  
                    FindObjectOfType<AudioManager>().Stop("SlowTime");
                    PlayerPrefs.SetFloat("CoinMagnet", 0F);
                    FindObjectOfType<AudioManager>().Stop("CoinMagnet");
                    Time.timeScale = 0;
                    SceneManager.LoadScene("Scenes/MenuFinal");
                }

            }
        }

        if (collider.gameObject.CompareTag("Moeda"))
        {
            coins++;
            collider.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Coin");      
            coinsText.text = "Coins: " + coins.ToString();
            if (coins >= 5) 
            {
                Time.timeScale = 0;
                coins = 0;
                PlayerPrefs.SetFloat("OnGame", 0);
                coinsText.text = "Coins: " + coins.ToString();
                canvasPowerUp.gameObject.SetActive(true);

                PowerUpsMenu powerUpsMenu = canvasPowerUp.GetComponent<PowerUpsMenu>();
                if (powerUpsMenu != null) 
                {
                    powerUpsMenu.AssignRandomPowerUps();
                }
            }
        }
    }

    public void OnJumpTap()
    {   
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        FindObjectOfType<AudioManager>().Play("Jump");        
    }
}
