using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public int speed = 1;
    public float transitionSpeed = 3.0f;
    public float amplitude = 0.5f;
    public float frequency = 0.5f;

    private int playerRoad = 2;
    private bool leftHeld = false;
    private bool rightHeld = false;
    private bool shiftingLeft = false;
    private bool shiftingRight = false;
    private float changeStartingTime;
    private Vector3 offset = new Vector3();

    // Use this for initialization
    void Start () {
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
