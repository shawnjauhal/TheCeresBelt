using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private GameObject galaxy;
    private GameObject ship;
    private GameObject stars;
    // Start is called before the first frame update
    void Start()
    {
        galaxy = GameObject.FindGameObjectWithTag("Galaxy");
        ship = GameObject.FindGameObjectWithTag("Ship");
        stars = GameObject.FindGameObjectWithTag("Stars");
    }

    // Update is called once per frame
    void Update()
    {
        // Move background along z-axis to match the speed of the ship
        int shipLocation = (int)ship.transform.position.z;
        galaxy.transform.position = new Vector3(0, 0, shipLocation + 800);
        stars.transform.position = new Vector3(0, -2861, shipLocation + 800);
        int galLocation = (int)galaxy.transform.position.z;
    }
}
