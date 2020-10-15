using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ShipMovement : MonoBehaviour
{
    public CharacterController controller;
    private GameObject ship;
    private GameObject GameOverMenu;
    private Boolean moving;
    private GameObject timeDisplay, speedDisplay;
    private float turnSpeed = 60f;
    private float seconds;
    private float forwardSpeed = 80f;
    private float xRot, zRot;
    private bool upwards, left;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        timeDisplay = GameObject.FindGameObjectWithTag("Time");
        speedDisplay = GameObject.FindGameObjectWithTag("Speed");
        GameOverMenu = GameObject.FindGameObjectWithTag("GameOver");
        GameOverMenu.SetActive(false);

        moving = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        Vector3 forward;
        
        seconds += Time.deltaTime;
        time += Time.deltaTime;

        // Get Horizontal and Vertical input from keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Set X and Y bound for ship movement
        if ((ship.transform.position.x > 900 && horizontal > 0) 
            || (ship.transform.position.x < -900 && horizontal < 0))
            horizontal = 0;
        if ((ship.transform.position.y > 483 && vertical > 0) 
            || (ship.transform.position.y < -483 && vertical < 0))
            vertical = 0;

        // Determine direction of movement if ship has not collided
        if (moving) {
            direction = new Vector3(horizontal, vertical, 0f).normalized;
            forward = new Vector3(0f, 0f, 1f);
        }
        else {
            vertical = 0f;
            horizontal = 0f;
            direction = new Vector3(0f, 0f, 0f);
            forward = new Vector3(0f, 0f, 0f);
        }

        // Forward Movement
        controller.Move(forward * forwardSpeed * Time.deltaTime);

        // Directional Movement
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * turnSpeed * Time.deltaTime);
        }

        // Determine rotation if the ship is moving up or down
        if (xRot <= 30 && xRot >= -30 && vertical != 0f) {
            xRot -= vertical * 250 * Time.deltaTime; 
            if (xRot > 30)
                xRot = 30;
            else if (xRot < -30)
                xRot = -30;
        }
        
        // Return to original rotation if no vertical input
        else if (vertical == 0f) {
            if (xRot < 0) 
                xRot += 300 * Time.deltaTime;
            else if (xRot > 0) 
                xRot -= 300 * Time.deltaTime;
            if (Math.Abs(xRot) <= 3.5) 
                xRot = 0;
        }

        // Determine rotation if the ship is moving left or right
        if (zRot <= 30 && zRot >= -30  && horizontal != 0) {
            zRot -= horizontal * 250 * Time.deltaTime;
            if (zRot > 30)
                zRot = 30;
            else if (zRot < -30)
                zRot = -30;
        }

        // Return to original rotation if no horizontal input
        else if (horizontal == 0f) {
            if (zRot < 0) 
                zRot += 400 * Time.deltaTime;
            else if (zRot > 0) 
                zRot -= 400 * Time.deltaTime;
            if (Math.Abs(zRot) <= 3.5) 
                zRot = 0;
        }

        // Apply rotation
        transform.localRotation = Quaternion.Euler(xRot, 0, zRot);

        // Increase forward speed 20% and turn speed 10% every 5 seconds
        if ((int)time % 5 == 0 && (int)time != 0 && forwardSpeed < 3000) {
            time -= 5;
            forwardSpeed = forwardSpeed * 1.2f;
            turnSpeed = turnSpeed * 1.1f;
        } 

        // Keep at max speed
        if (forwardSpeed > 3000) {
            forwardSpeed = 3000f;
        }

        // Display current speed and time in HUD
        TimeSpan tspan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        String output = tspan.ToString(@"hh\:mm\:ss\.ff");
        if (moving) {
            timeDisplay.GetComponent<Text>().text = "Time:  " + output;
            speedDisplay.GetComponent<Text>().text = "Speed: " + Math.Round((forwardSpeed / 10), 2) + "km/s";
        }
    }
    
    private void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.gameObject.tag == "Asteroid") {
            moving = false;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        GameOverMenu.SetActive(true);
    }
    
}
