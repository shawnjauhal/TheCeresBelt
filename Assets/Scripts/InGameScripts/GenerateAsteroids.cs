using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{

    public GameObject asteroid, ship;
    private int xPos, yPos, zPos;
    private float randX, randY, randZ;
    private int asteroidCount;
    private List<GameObject> asteroids = new List<GameObject>();
    private float offset = 14000;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        StartCoroutine(AsteroidSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        int shipLocation = (int)ship.transform.position.z;
        
        // Create shallow copy to loop through and remove asteroids behind ship
        GameObject[] asteroidsCopy = asteroids.ToArray();
        foreach (GameObject ast in asteroidsCopy)
        {
            int asteroidLocation = (int)ast.transform.position.z;
            if (asteroidLocation < shipLocation - 100)
            {
                asteroids.Remove(ast);
                Destroy(ast);
                xPos = Random.Range(-1000, 1000);
                yPos = Random.Range(-562, 562);
                zPos = (int)Random.Range(350 + offset, 5150 + offset);
                zPos += shipLocation;
                Vector3 coordinates = new Vector3(xPos, yPos, zPos);
                GameObject newAstroid = Instantiate(asteroid, coordinates, Quaternion.identity);
                asteroids.Add(newAstroid);
            }
        }     
    }

    IEnumerator AsteroidSpawn() 
    {
        while (asteroidCount < 1750) {
            xPos = Random.Range(-1000, 1000);
            yPos = Random.Range(-562, 562);
            zPos = Random.Range(2050, 16350);
            randX = Random.Range(.8f, 1.2f) * 100;
            randY = Random.Range(.8f, 1.2f) * 140;
            randZ = Random.Range(.8f, 1.2f) * 100;
            Vector3 coordinates = new Vector3(xPos, yPos, zPos);
            Vector3 newSize = new Vector3(randX, randY, randZ);
            GameObject ast = Instantiate(asteroid, coordinates, Quaternion.identity);
            ast.transform.localScale = newSize;
            asteroids.Add(ast);
            asteroidCount += 1;
        }
        yield return new WaitForSeconds(.0000001f);
    }
}
