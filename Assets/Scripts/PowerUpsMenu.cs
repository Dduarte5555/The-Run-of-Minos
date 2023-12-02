using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class PowerUpInfo
{
    public string powerUpName;
    public Sprite powerUpImage;
}

public class PowerUpsMenu : MonoBehaviour
{
    
    public Canvas canvas;
    public GameObject[] powerUpCombos;
    public List<PowerUpInfo> powerUpInfoList; 

    public void AssignRandomPowerUps()
    {
        // Shuffle the available power-ups list
        Shuffle(powerUpInfoList);

        // Assign power-ups to buttons
        for (int i = 0; i < powerUpCombos.Length; i++)
        {
            string powerUpName = powerUpInfoList[i].powerUpName;

            // Get the Button component from PowerUpCombo
            Button button = powerUpCombos[i].GetComponentInChildren<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = powerUpName;

            // Set the button text to the power-up name
            powerUpCombos[i].GetComponentInChildren<TextMeshProUGUI>().text = powerUpName;

            // Remove previous listeners
            button.onClick.RemoveAllListeners();

            // Assign the corresponding onClick method based on the power-up name
            switch (powerUpName)
            {
                case "Slow Time":
                    button.onClick.AddListener(OnSlowButton);
                    break;
                case "Dash":
                    button.onClick.AddListener(OnDashButton);
                    break;
                case "Invincible":
                    button.onClick.AddListener(OnInvincibleButton);
                    break;
                case "Coin Magnet":
                    button.onClick.AddListener(OnCoinMagnetButton);
                    break;
                default:
                    break;
            }

            Image[] imagesInChildren = powerUpCombos[i].GetComponentsInChildren<Image>(true);
            if (imagesInChildren.Length > 1)
            {
                Image imgPowerUp = imagesInChildren[1];

                imgPowerUp.sprite = powerUpInfoList[i].powerUpImage;
                imgPowerUp.preserveAspect = true; 

                // Add a pointer click event to the Image
                EventTrigger eventTrigger = imgPowerUp.gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.RemoveAllListeners();
                entry.callback.AddListener((eventData) => { InvokePowerUpOnClick(powerUpName); });
                eventTrigger.triggers.Add(entry);
            }
        }
    }

    void InvokePowerUpOnClick(string powerUpName)
    {
        // Find the corresponding button and invoke its onClick event
        GameObject buttonObject = Array.Find(powerUpCombos, obj => obj.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text == powerUpName);
        if (buttonObject != null)
        {
            Button button = buttonObject.GetComponentInChildren<Button>();
            button.onClick.Invoke();
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

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
        FindObjectOfType<AudioManager>().Play("Shield");
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
        FindObjectOfType<AudioManager>().Play("CoinMagnet");
        PlayerPrefs.SetFloat("CoinMagnet", 1f);
        float time = PlayerPrefs.GetFloat("SavedTime", 1.0f);
        Time.timeScale = time;
        PlayerPrefs.SetFloat("SavedTime_powerup", 0);
        PlayerPrefs.SetFloat("OnGame", 1);
        PlayerPrefs.Save(); 
        canvas.gameObject.SetActive(false);
    }

}
