using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
