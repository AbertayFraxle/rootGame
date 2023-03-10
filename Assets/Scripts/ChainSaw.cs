using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChainSaw : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    
    [SerializeField] private List<GameObject> legs;

    [SerializeField] private GameObject player;

    private livesManager livesMan;

    private bool isWaiting;

    private void Start()
    {
       particle.SetActive(false);
       isWaiting = false;
        livesMan = player.GetComponent<livesManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
        {
            ScreenShake.Instance.ShakeCamera(10f, 0.2f);
            if (!player.GetComponent<pickupManager>().isSunMode())
            {
                
                particle.SetActive(true);
                if (!isWaiting)
                {
                    StartCoroutine(CutLeg());
                }
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            particle.SetActive(false);
        } 
    }

    private void Update()
    {
        if (legs.Count <= 0)
        {
            livesMan.rootDeath();
        }
    }

    /// <summary>
    /// Cut leg and then wait three seconds until it can be done again. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CutLeg()
    {
        isWaiting = true;
        
        //get random leg to remove
        int legIndex = Random.Range(0, legs.Count);
        var legToRemove = legs[legIndex];

        //disable hinge joint
        legToRemove.GetComponent<HingeJoint2D>().enabled = false;
        
        //unparent
        legToRemove.transform.parent = null;
        
        //remove leg
        legs.RemoveAt(legIndex);
        
        yield return new WaitForSecondsRealtime(3f);

        isWaiting = false;
    }
    
}
