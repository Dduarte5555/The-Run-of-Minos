using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerUpsMenu : MonoBehaviour
{
    
    public Canvas canvas;
    public void OnSlowButton ()
    {
        FindObjectOfType<AudioManager>().Play("SlowTime");
        Time.timeScale = 0.5F;
        PlayerPrefs.SetFloat("SavedTime_powerup", 0);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save();
        canvas.gameObject.SetActive(false); 
    }

    public void OnDashButton()
    {
        PlayerPrefs.SetFloat("Dash", 1F);
        PlayerPrefs.SetFloat("Invincible", 1F);
        Time.timeScale = 10F;
        PlayerPrefs.SetFloat("SavedTime_powerup", 0);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save(); 
        canvas.gameObject.SetActive(false);
    }

    public void OnInvincibleButton()
    {
        FindObjectOfType<AudioManager>().Play("Shield");
        PlayerPrefs.SetFloat("Invincible", 1F);
        PlayerPrefs.Save();
        float time = PlayerPrefs.GetFloat("SavedTime", 1.0f);
        Time.timeScale = time;
        PlayerPrefs.SetFloat("SavedTime_powerup", 0);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save();
        canvas.gameObject.SetActive(false);
    }

    public void OnNo()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");  
        Time.timeScale = 0;
        SceneManager.LoadScene("Scenes/MenuFinal");
    }

     public void OnCoinMagnetButton()
    {
        PlayerPrefs.SetFloat("CoinMagnet", 1f);
        float time = PlayerPrefs.GetFloat("SavedTime", 1.0f);
        Time.timeScale = time;
        PlayerPrefs.SetFloat("SavedTime_powerup", 0);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save(); 
        canvas.gameObject.SetActive(false);
    }

}
