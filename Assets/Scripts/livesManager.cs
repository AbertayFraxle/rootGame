using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class livesManager : MonoBehaviour
{
    public int maxLives = 3;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            //game over
            print("player has died");
        }
    }

    public void addLife()
    {
        if (lives < maxLives)
        {
            lives++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "fire")
        {
            if (!this.GetComponent<pickupManager>().isSunMode())
            {
                lives--;
            }
        }
    }
}
