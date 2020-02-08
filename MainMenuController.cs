using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


//References for each of the buttons in game

    public GameObject PauseMenu;
    public GameObject Instructions;

    public GameObject arrowUp;
    public GameObject arrowDown;
    public GameObject bigJump;

//Loading scenes or pausing game functions
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void HighscoreMenu()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void QuitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void PauseBtn()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        arrowUp.SetActive(false);
        arrowDown.SetActive(false);
        bigJump.SetActive(false);

    }

    public void UnpauseBtn()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        arrowUp.SetActive(true);
        arrowDown.SetActive(true);
        bigJump.SetActive(true);
    }

    public void HowToPlay()
    {
        Instructions.SetActive(true);
    }

    public void HowNotToPlay()
    {
        Instructions.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
