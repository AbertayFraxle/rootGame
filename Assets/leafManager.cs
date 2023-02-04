using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class leafManager : MonoBehaviour
{
    [SerializeField] public GameObject brain;

    private livesManager lives;

    int leafCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        lives= brain.GetComponent<livesManager>();

        foreach (Transform child in transform)
        {
            if (child.tag == "leaf")
            {
                leafCount++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;

        int perLife = leafCount/ lives.maxLives;

        int maxIndex = lives.lives* perLife;

        foreach (Transform child in transform)
        {
            if (child.tag == "leaf")
            {
                counter++;

                if (counter > maxIndex)
                {
                    child.localScale = Vector3.zero;
                }else
                {
                    child.localScale = Vector3.one;
                }

                

                
            }
        }
    }
}
