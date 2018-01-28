using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameOverMenuController : MonoBehaviour {

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

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
