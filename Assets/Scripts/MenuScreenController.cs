using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuScreenController : MonoBehaviour {

    //GameObject[] oldObjects = GameObject.FindGameObjectsWithTag("Token");

    public AudioSource soundSource;

    private void Start()
    {
        soundSource = gameObject.GetComponent<AudioSource>();
        soundSource.Play();
    }

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