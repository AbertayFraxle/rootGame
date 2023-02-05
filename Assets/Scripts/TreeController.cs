using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    //root from left to right, 1 to 5.
    [SerializeField] private List<SpringJoint2D> rootJoints;
    
    //this controls the distance of the spring. The force the leg sprung up. 
    [SerializeField] private float springDist = 100f;
    private float currentDist;

    [SerializeField] public GameObject main;

    //audio source and clips
    [SerializeField] private AudioSource audioScr;
    [SerializeField] private List<AudioClip> playerSfx;

    //holds the keys for extending roots 1-5
    [SerializeField] private List<KeyCode> rootKeys;

    private livesManager lManager;

    //public as pickups will use this too
    public void playTreeSound(int id)
    {
        audioScr.PlayOneShot(playerSfx[id]);
    }

    private void Start()
    {
        lManager = main.GetComponent<livesManager>();
    }

    private void Update()
    {
        if (main.GetComponent<pickupManager>().isSunMode())
        {
            currentDist = 2 * springDist;
        }
        else
        {
            currentDist = springDist;
        }

        if (lManager.lives > 0)
        {

            for (int i = 0; i < rootKeys.Count; i++)
            {
                rootJoints[i].distance = Input.GetKey(rootKeys[i]) ? springDist : 0;

                //if key gets pressed, play corrisponding sfx (each of the 5 roots has a different sfx)
                if (Input.GetKeyDown(rootKeys[i]))
                {
                    playTreeSound(i);
                }
            }
        }
    }
    
}
