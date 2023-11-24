using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Menu_power_up : MonoBehaviour
{
    // public TextMeshProUGUI scoreText;
    // public TextMeshProUGUI maxScoreText;
    private float time;

    void Start()
    {
        time = PlayerPrefs.GetFloat("SavedTime", 1.0f);
        // int score = PlayerPrefs.GetInt("Score", 0);
        // int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        // if (scoreText) 
        // {
        //     scoreText.text = "Score: " + score.ToString();
        // }

        // if (maxScoreText) 
        // {
        //     maxScoreText.text = "Max score: " + maxScore.ToString();
        // }
    }
    public void OnSlowButton ()
    {
        // FindObjectOfType<AudioManager>().Play("Play");
        Debug.Log("Time: " + time);
        time = time * 1.1F;
        PlayerPrefs.SetFloat("SavedTime", time);
        PlayerPrefs.Save(); // Isso salva as PlayerPrefs imediatamente
        Time.timeScale = time;
        Debug.Log("Time: " + time);
        SceneManager.LoadScene("Scenes/MapDevelopment");
    }
}
