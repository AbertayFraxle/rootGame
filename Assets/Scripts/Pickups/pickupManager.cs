using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class pickupManager : MonoBehaviour
{
    [SerializeField] public TreeController treeCon;

    //mario
    [SerializeField] public jumpManager jumpMan;

    bool sun;
    float sunTimer;

    public float sunLimit = 5;

    public int maxJumps = 2;
    int jumps;

    // Start is called before the first frame update
    void Start()
    {
        sun = false;
        sunTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if the sun is picked up, 
        if (sun)
        {
            sunTimer = sunLimit;
            sun = false;
        }
        else if (sunTimer > 0)
        {
            sunTimer -= Time.deltaTime;
            if (sunTimer <= 0)
            {
                print("sun effect ended");
            }
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if this is an interactable, set it to only be triggered once
        pickupPlayer picked = other.GetComponent<pickupPlayer>();

        if (picked)
        {
            

            if (!picked.isPicked())
            {
                picked.setPicked();
                switch (other.tag)
                {
                    case "sun":
                        //trigger a super-powered mode for 5 seconds
                        sun = true;
                        treeCon.playTreeSound(7);
                        print("sun object hit");
                        break;
                    case "gas":
                        //increase jumps
                        if (jumps < maxJumps)
                        {
                            jumps++;
                        }
                        treeCon.playTreeSound(8);
                        print("gas canister collected");
                        break;
                    case "water":
                        this.GetComponent<livesManager>().addLife();
                        treeCon.playTreeSound(6);
                        print("water collected");
                        break;
                    case "boost":
                        jumpMan.doBoost();
                        treeCon.playTreeSound(9);
                        print("boost collected");
                        break;
                }
            }
            Destroy(other.gameObject);
        }
        
    }

    public bool isSunMode()
    {
        if (sunTimer > 0)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public bool tryJump()
    {
        if (jumps > 0)
        {
            jumps--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
