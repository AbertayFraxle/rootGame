using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class livesManager : MonoBehaviour
{
    public int maxLives = 3;
    public int lives;
    public float invuln;
    public float maxInvuln = 1;

    [SerializeField] GameObject root;
    [SerializeField] UIManager uiMan;

    // Start is called before the first frame update
    void Start()
    {
        lives = maxLives;
        invuln = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (invuln > 0)
        {
            invuln -= Time.deltaTime;
        }

        if (lives <= 0)
        {
            //game over
            uiMan.showGameOverScreen();
            
        }
    }

    public void addLife()
    {
        if (lives < maxLives)
        {
            lives++;
        }
    }

    public int getLife()
    {
        return lives;
    }

    public void rootDeath()
    {
        lives = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "fire")
        {
            if (!this.GetComponent<pickupManager>().isSunMode())
            {
                if (invuln <= 0)
                {
                  //  lives--;
                    ScreenShake.Instance.ShakeCamera(5f,1f);
                    invuln = maxInvuln;
                }
            }
        }
    }
}
