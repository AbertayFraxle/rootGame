using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EvilSpawner : MonoBehaviour
{
    float spawnTimer;

    public GameObject sun, gas, water;

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
                int spawnChance = Random.Range(0, 3);

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
                }
                Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, Camera.main.nearClipPlane));


                GameObject spawned = Instantiate(currentSpawn, new Vector3(spawnPoint.x, 15.0f, 0), new Quaternion());

                nextPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.6f, 0, Camera.main.nearClipPlane)).x;

            }

            spawnTimer = Random.Range(10, 15);


        }


    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
       // Gizmos.DrawSphere(, 2);
    }

}



