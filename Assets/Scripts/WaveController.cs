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

    private int playerRoad = 2;
    private bool leftHeld = false;
    private bool rightHeld = false;
    private bool shiftingLeft = false;
    private bool shiftingRight = false;
    private bool invincible = false;
    private float changeStartingTime;
    private Vector3 offset = new Vector3();

    // Use this for initialization
    void Start () {
        soundSource = gameObject.GetComponents<AudioSource>();
        offset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        var pos = transform.position;
        pos.y = offset.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        pos.z += speed * 0.3f;
        transform.position = pos;
        //if (Input.GetAxis("Horizontal") < 0 && !leftHeld)
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            if (playerRoad == 2 || playerRoad == 3)
            {
                playerRoad--;
                pos.x -= 6;
                transform.position = pos;
            }
        }
        //if (Input.GetAxis("Horizontal") > 0 && !rightHeld)
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            if (playerRoad == 1 || playerRoad == 2)
            {
                playerRoad++;
                pos.x += 6;
                transform.position = pos;
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
