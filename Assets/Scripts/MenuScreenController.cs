using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour {

    //GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Token");

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void StartInstructions()
    {
        SceneManager.LoadScene("InstructionsMenu");
    }

    public void StartCredits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void StartGameOver()
    {
        SceneManager.LoadScene("GameOverMenu");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}