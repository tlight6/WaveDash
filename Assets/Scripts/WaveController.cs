using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class WaveController : MonoBehaviour {

    public float speed = 1.0f;
    public float transitionSpeed = 3.0f;
    public float amplitude = 0.5f;
    public float frequency = 0.5f;
    public AudioSource[] soundSource;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    public Sprite health0;
    public int health = 3;
    public Image healthMeter;
    public Material hurtMaterial;
    public Material normalMaterial;
    public Text scoreText;
    public int score = 0;

    private int totalTokens;
    private int playerRoad = 2;
    private bool leftHeld = false;
    private bool rightHeld = false;
    private bool shifting = false;
    private bool invincible = false;
    private float changeStartingTime;
    private Vector3 offset = new Vector3();

    // Use this for initialization
    void Start () {
        soundSource = gameObject.GetComponents<AudioSource>();
        offset = transform.position;
        totalTokens = GameObject.FindGameObjectsWithTag("Token").Length;
        scoreText.text = "Score: " + score.ToString() + "/" + totalTokens.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        var pos = transform.position;
        pos.y = offset.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        pos.z += speed * 0.3f;
        transform.position = pos;
        //if (Input.GetAxis("Horizontal") < 0 && !leftHeld)
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && !shifting)
        {
            if (playerRoad == 2 || playerRoad == 3)
            {
                StartCoroutine(ShiftLeft());
            }
        }
        //if (Input.GetAxis("Horizontal") > 0 && !rightHeld)
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !shifting)
        {
            if (playerRoad == 1 || playerRoad == 2)
            {
                StartCoroutine(ShiftRight());
            }
        }
        leftHeld = (Input.GetAxis("Horizontal") < 0);
        rightHeld = (Input.GetAxis("Horizontal") > 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Token"))
        {
            Destroy(other.gameObject);
            soundSource[0].Play();
            score++;
            scoreText.text = "Score: " + score.ToString() + "/" + totalTokens.ToString();
        }
        else if (other.gameObject.CompareTag("Negative Token") && health >= 1 && !invincible)
        {
            Destroy(other.gameObject);
            soundSource[1].Play();
            health--;
            if (health == 2)
            {
                healthMeter.sprite = health2;
                StartCoroutine(Flasher());
                StartCoroutine(BuildSpeed());
            }
            else if (health == 1)
            {
                healthMeter.sprite = health1;
                StartCoroutine(Flasher());
            }
            else if (health == 0)
            {
                healthMeter.sprite = health0;
                StartCoroutine(Flasher());
            }
        }
    }

    IEnumerator Flasher()
    {
        invincible = true;
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().material = hurtMaterial;
            yield return new WaitForSeconds(.12f);
            GetComponent<Renderer>().material = normalMaterial;
            yield return new WaitForSeconds(.12f);
        }
        invincible = false;
    }

    IEnumerator BuildSpeed()
    {
        float initialSpeed = speed;
        speed = initialSpeed * 0.2f;
        while (speed < initialSpeed/2)
        {
            speed += Time.deltaTime * 1f * initialSpeed;
            yield return new WaitForSeconds(0.0f);
        }
        while (speed < initialSpeed)
        {
            speed += Time.deltaTime * 2f * initialSpeed;
            yield return new WaitForSeconds(0.0f);
            speed = Mathf.Min(speed, initialSpeed);
        }
    }

    IEnumerator ShiftLeft()
    {
        shifting = true;
        playerRoad--;
        var pos = transform.position;
        float dist = 0f;
        float currDist = 0f;
        while (dist < 6)
        {
            pos.y = offset.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            pos.z += speed * 0.3f;
            currDist = Time.deltaTime * 25f;
            dist += currDist;
            pos.x -= currDist;
            yield return new WaitForSeconds(0.0f);
            transform.position = pos;
        }
        if (playerRoad == 1)
        {
            pos.x = -6;
            transform.position = pos;
        }
        else if (playerRoad == 2)
        {
            pos.x = 0;
            transform.position = pos;
        }
        shifting = false;
    }

    IEnumerator ShiftRight()
    {
        shifting = true;
        playerRoad++;
        var pos = transform.position;
        float dist = 0f;
        float currDist = 0f;
        while (dist < 6)
        {
            pos.y = offset.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            pos.z += speed * 0.3f;
            currDist = Time.deltaTime * 25f;
            dist += currDist;
            pos.x += currDist;
            yield return new WaitForSeconds(0.0f);
            transform.position = pos;
        }
        if (playerRoad == 2)
        {
            pos.x = 0;
            transform.position = pos;
        }
        else if (playerRoad == 3)
        {
            pos.x = 6;
            transform.position = pos;
        }
        shifting = false;
    }

    /*void MoveLeft()
    {
        if (playerRoad == 2 || playerRoad == 3)
        {
            playerRoad--;
            var pos = transform.position;
            pos.x -= transitionSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    void MoveRight()
    {
        if (playerRoad == 1 || playerRoad == 2)
        {
            playerRoad++;
            var pos = transform.position;
            pos.x += 6;
            transform.position = pos;
        }
    }*/
}
