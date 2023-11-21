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
        int score = PlayerPrefs.GetInt("Score", 0);
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

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
        SceneManager.LoadScene("Scenes/MapDevelopment");
    }
}
