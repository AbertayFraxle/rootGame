using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{

    float spawnTimer;

    public GameObject sun, gas, water, boost;

    GameObject currentSpawn;

    float nextPoint;


    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(10, 15);
        nextPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        float currPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;


        if (spawnTimer <= 0)
        {
            if (currPoint >= nextPoint)
            {
                int spawnChance = Random.Range(0, 4);

                switch (spawnChance)
                {
                    case 0:
                        currentSpawn = sun;
                        break;
                    case 1:
                        currentSpawn = gas;
                        break;
                    case 2:
                        currentSpawn = water;
                        break;
                    case 3:
                        currentSpawn = boost;
                        break;
                }
                Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0.5f, 0.9f), Camera.main.nearClipPlane));

                GameObject spawned = Instantiate(currentSpawn, new Vector3(spawnPoint.x, spawnPoint.y, 0),new Quaternion());

                nextPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.6f, 0, Camera.main.nearClipPlane)).x;

            }

            spawnTimer = Random.Range(10, 15);


        }


    }

}



