using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayButton : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Pontuacao", 0);
        int maxScore = PlayerPrefs.GetInt("Recorde", 0);
        PlayerPrefs.SetInt("Pontuacao", 0);
    
        if (score > maxScore){
            maxScore = score;
            PlayerPrefs.SetInt("Recorde", score);
        }

        if (scoreText) 
        {
            scoreText.text = "Score: " + score.ToString();
        }

        if (maxScoreText) 
        {
            maxScoreText.text = "Max score: " + maxScore.ToString();
        }
    }
    public void OnPlayButton ()
    {
        FindObjectOfType<AudioManager>().Play("Play");
        PlayerPrefs.SetFloat("SavedTime", 1.0f);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("OnGame", 1);
        SceneManager.LoadScene("Scenes/MapDevelopment");

    }
}
