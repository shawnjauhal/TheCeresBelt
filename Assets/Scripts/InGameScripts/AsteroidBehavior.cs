using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    float xRot, yRot, zRot, randX, randY, randZ;
    // Start is called before the first frame update
    void Start()
    {
        // Generates random numbers for the speed of rotation
        randX = Random.Range(1, 15);
        randY = Random.Range(1, 15);
        randZ = Random.Range(1, 15);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates astroid at random angle
        xRot += randX * Time.deltaTime;
        yRot += randY * Time.deltaTime;
        zRot += randZ * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }
}
