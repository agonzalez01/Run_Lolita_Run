using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{

//References to high score texts in game
    public Text high1;
    public Text high2;
    public Text high3;



    int highScore1, highScore2, highScore3;

 
    

    void Start()
    {

//Setting texts in game 
        high1.text = PlayerPrefs.GetInt("highScore1").ToString();
        high2.text = PlayerPrefs.GetInt("highScore2").ToString();
        high3.text = PlayerPrefs.GetInt("highScore3").ToString();

    }

    public void highScore(int score) //Checking if the score from each game is greater than any of the current high scores 
    {
        if (score > PlayerPrefs.GetInt("highScore1"))
        {
            PlayerPrefs.SetInt("highScore3", PlayerPrefs.GetInt("highScore2"));
            PlayerPrefs.SetInt("highScore2", PlayerPrefs.GetInt("highScore1"));
            PlayerPrefs.SetInt("highScore1", score);

        }

        else if (score < PlayerPrefs.GetInt("highScore1") && score > PlayerPrefs.GetInt("highScore2"))
        {
            PlayerPrefs.SetInt("highScore3", PlayerPrefs.GetInt("highScore2"));
            PlayerPrefs.SetInt("highScore2", score);            
        }

        else if (score < PlayerPrefs.GetInt("highScore2") && score > PlayerPrefs.GetInt("highScore3"))
        {
            PlayerPrefs.SetInt("highScore3", score);
        }

        
    }
}
